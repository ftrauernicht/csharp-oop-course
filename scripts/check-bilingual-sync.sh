#!/usr/bin/env bash
# Checks the repo's two bilingual conventions (see CONTRIBUTING.md):
#   1. courses/*/en/ and courses/*/de/ must have chapters with matching
#      numeric prefixes (filenames themselves are translated, e.g.
#      00-introduction.md <-> 00-einfuehrung.md).
#   2. Root-level docs follow NAME.md (English) + NAME.de.md (German),
#      except CLAUDE.md, which has no reader-facing German counterpart.
set -euo pipefail
shopt -s nullglob

fail=0

for course_dir in courses/*/; do
  en_dir="${course_dir}en"
  de_dir="${course_dir}de"

  if [[ ! -d "$en_dir" || ! -d "$de_dir" ]]; then
    echo "::error::${course_dir} is missing an en/ or de/ folder"
    fail=1
    continue
  fi

  en_prefixes=$(for f in "$en_dir"/*.md; do basename "$f"; done | grep -oE '^[0-9]+' | sort -u || true)
  de_prefixes=$(for f in "$de_dir"/*.md; do basename "$f"; done | grep -oE '^[0-9]+' | sort -u || true)

  if [[ "$en_prefixes" != "$de_prefixes" ]]; then
    echo "::error::${course_dir}: en/ and de/ chapter numbers differ"
    echo "  en: $(echo "$en_prefixes" | tr '\n' ' ')"
    echo "  de: $(echo "$de_prefixes" | tr '\n' ' ')"
    fail=1
  fi
done

for f in *.md; do
  [[ "$f" == *.de.md ]] && continue
  [[ "$f" == "CLAUDE.md" ]] && continue
  de_file="${f%.md}.de.md"
  if [[ ! -f "$de_file" ]]; then
    echo "::error::${f} has no German counterpart (${de_file})"
    fail=1
  fi
done

for f in *.de.md; do
  en_file="${f%.de.md}.md"
  if [[ ! -f "$en_file" ]]; then
    echo "::error::${f} has no English counterpart (${en_file})"
    fail=1
  fi
done

if [[ "$fail" -eq 0 ]]; then
  echo "Bilingual sync check passed."
fi

exit "$fail"

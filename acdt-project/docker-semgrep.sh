docker run --rm -v ".:/src" returntocorp/semgrep semgrep --config auto --output scan_results.json --json  --exclude "bin/*" --exclude "*.dll" --exclude "obj/*"

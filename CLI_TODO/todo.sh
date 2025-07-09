cat > ~/bin/todo <<'EOF'
#!/usr/bin/env bash

# go to the publish directory
cd "$HOME/todoCLI/CLI_TODO/bin/Release/net9.0/publish" || {
  echo "❌ Publish directory not found!"
  exit 1
}

# run the app, forwarding any arguments
exec dotnet CLI_TODO.dll "$@"
EOF
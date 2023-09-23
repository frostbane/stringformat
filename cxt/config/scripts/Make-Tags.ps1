#!/usr/bin/env -S pwsh -NoLogo -NoProfile


ctags `
    -R `
    --exclude=.hg `
    --exclude=.vscode `
    --exclude=./app/**/bin `
    --exclude=./app/**/obj `
  .


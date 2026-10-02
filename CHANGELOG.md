# Changelog

All notable changes to this project are documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

## [0.0.9] - 2026-09-21

### Added

- Validate required repository files, navigation links, and Solution Items in CI.

### Fixed

- Use framework-specific null guards and allow scenario-based names on test classes.

- Validate required command and pipeline delegate arguments before dispatch.

- Disambiguate exception references in generated XML documentation.

- Await external-event pipeline handlers to completion and reject events that complete without invoking their action.

- Correct the repository guide to reflect that central package management is not used.

- Include the build configuration in CI artifact names to prevent collisions between Revit matrix jobs.

### Changed

- Standardize GitHub Actions workflow filenames and display names by responsibility.

- Limit the declared Revit support matrix to 2021.1.9, 2023.0.0, and 2025.0.0.

- Configure xUnit v3 test execution through Microsoft.Testing.Platform and fail test runs when no tests are discovered.

- Establish a shared EditorConfig baseline and use the repository-policy validator as the single structural CI check.

- Complete solution items for repository documents, configuration, workflows and maintenance scripts; document the shared layout.

- Standardize local and CI SDK selection on stable .NET 10.0 through global.json, restrict roll-forward to that major/minor line, and configure setup-dotnet to read the file.

- Show documentation in Visual Studio Solution Explorer under a `docs` solution folder with matching subfolders.

- Move development policies and repository guidance from `AGENTS.md` to `docs/policies/development.md` and `docs/repository.md`; keep required reading links in `AGENTS.md` and add README navigation.

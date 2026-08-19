# AP-Tracker (Legacy)

> ⚠️ **Status: Broken / Unmaintained** — This is the predecessor to the current [AP Tracker] project. It no longer builds/runs correctly and is kept around for reference.

## What this is

An earlier Unity-based version of AP Tracker. This project has since been superseded by a newer implementation, but the code is kept here for reference — parts of the approach, structure, or lessons learned here informed the rewrite.

## Why it's broken

This project depends on **OpenCvSharp** for computer vision processing, but the version available does not support the chip architecture used by modern smartphones — which is the actual target platform for this project. As a result:

- [ ] Builds may work in the Unity Editor but fail (or silently break CV features) on-device

## Tech stack

- **Engine:** Unity
- **Computer Vision:** OpenCvSharp *(incompatible with target chip architecture — this is the blocker)*

## Relation to current project

This repo is the **predecessor** to the actively maintained AP Tracker project. If you're looking for the working version, go there instead — this repo is not receiving further updates.

## Known issues

- OpenCvSharp lacks a build compatible with modern smartphone chipsets, breaking the core CV functionality this project depends on.
- UX/UI is outdated/mediocre

## Why keep this repo

Kept for historical reference and to document what was tried before the rewrite — useful if the new project ever needs to revisit design decisions made here.

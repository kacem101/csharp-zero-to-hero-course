# Prompt: Progress Tracker section for the course landing page

Use this with your design tool of choice. It describes what to build and
why, not exact visuals — leave the visual treatment to whatever tool
you're using, as long as it matches the existing page's dark,
terminal-adjacent aesthetic (see context below).

---

## Context (paste this in if the tool needs it)

I have a landing page for a C# course repo (kacem101/csharp-zero-to-hero-course)
in a dark theme with a terminal/developer aesthetic: background #0d0c0b,
surface #161412, accent orange #e8763a, JetBrains Mono for code/labels,
Inter for body text, flat surfaces with 1px borders (no shadows/gradients
except a subtle radial glow), 8px border radius on components. Sections
so far: hero, stats row, "what this is," six project rows, a code diff
(bad-vs-good SQL example), and a "getting started" terminal block.

## What I need

A new section — call it "Track your progress" — that introduces a tool
already in the repo at `tools/progress-tracker.html`: a self-contained,
single-file interactive checklist covering all 37 lessons + 6 projects,
grouped by module, with a progress bar and per-module completion counts.
It persists state between sessions (no login, no backend — it remembers
what you've checked off using in-browser storage).

This section's job: make a visitor who's about to clone the repo aware
this tool exists BEFORE they start, not discover it by accident on file
number 40.

## Functional requirements

- Explain what the tool is and where it lives (`tools/progress-tracker.html`)
  in one or two sentences — no marketing language, just what it does.
- Make clear it's optional but useful — this is a study aid, not a
  requirement to use the course.
- Include a link/CTA to the file in the GitHub repo:
  `https://github.com/kacem101/csharp-zero-to-hero-course/blob/master/tools/progress-tracker.html`
- Ideally, show a hint of what it looks like — not necessarily a full
  screenshot, but something (a small mock, a partial preview, an
  illustrative fragment of the checklist UI) that makes it feel real and
  not just described in prose.

## Content it should communicate (facts, not copy — write your own wording)

- Tracks all 37 lessons across 8 modules, plus the 6 projects
- Shows overall % complete and per-module breakdown
- State persists across sessions — closing the tab doesn't lose progress
- No sign-up, no account, nothing to install — it's an HTML file, open
  it locally or however you're serving it

## Placement

This should slot in as its own section, most naturally either:
(a) right after "How to start" (as the next logical step once someone's
    cloned the repo), or
(b) as a callout inside "How to start" itself, rather than a fully
    separate full-width section — this is a secondary feature, not a
    primary pillar of the page, so don't give it more visual weight than
    the six projects or the getting-started flow got.

## Tone

Same as the rest of the page: direct, no hype, describes what a thing
does rather than how amazing it is. "Tracks your progress across all 37
lessons, with state that persists between sessions" — not "Supercharge
your learning journey with our revolutionary progress system."

## What NOT to do

- Don't invent stats or claims about the tool that aren't listed above
  (no "used by thousands of students," no fake testimonials)
- Don't make this section bigger or more visually prominent than the
  Six Projects section — it's a nice-to-have utility, not the main pitch
- Don't require JavaScript trickery that would look out of place next to
  the rest of the page's restrained motion style

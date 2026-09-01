# RÉQUIEM — Decision Log

> Este arquivo registra as decisões mais recentes e **vence documentos antigos quando houver contradição**.

## 2026-08-14 — Escopo de personagem

**Decisão:** Noah é o único personagem jogável e a única presença aliada clara durante quase todo o jogo.

Consequências:

- sem elenco grande;
- sem hub cheio de NPCs;
- sem companheiro acompanhando Noah;
- qualquer voz humana ganha peso;
- a segunda pessoa aparece fisicamente apenas perto do final.

## 2026-08-14 — Segunda pessoa

**Decisão interna:** a segunda pessoa é **Elia**.

**Relação V1:** Elia é irmã de Noah.

Para o jogador, o nome e a relação são escondidos durante boa parte da história. Documentos públicos podem chamá-la apenas de “segunda presença”, mas isso é ocultação narrativa, não indefinição de produção.

A verdade completa está em `STORY_CORE_SPOILERS.md`.

## 2026-08-14 — Função das cartas

**Decisão:** cartas são **Memórias de Ação**, não itens mágicos, buffs passivos ou um card game separado.

- Noah se move diretamente;
- esquiva universal fica fora do baralho;
- quatro ações ofensivas ficam visíveis na mão;
- usar uma carta executa uma memória motora;
- redraw representa outra memória entrando em foco;
- RÉQUIEM representa coerência temporária dessas memórias.

## 2026-08-14 — Relação com ritmo

**Decisão:** Réquiem é action game primeiro e rhythm layer depois.

Nunca bloquear um ataque porque o jogador errou o beat.

Timing correto melhora:

- resposta audiovisual;
- propriedade da carta;
- Cadência.

## 2026-08-14 — Poder / “aura”

**Decisão:** grandes momentos vêm de controle e contraste, não excesso.

Prioridade:

1. antecipação;
2. pose/animação;
3. contato;
4. hit-stop;
5. som;
6. câmera;
7. partículas.

RÉQUIEM deve ficar visualmente **mais preciso e confiante**, não mais poluído.

## 2026-08-14 — História-base

**Decisão:** o Coração do Mundo começou a preservar Vesper em excesso, tornando memória e matéria rígidas demais para mudar.

Elia descobre o Último Acorde, capaz de encerrar essa preservação. Como ela já funciona como âncora do Coração, concluir o processo significa perdê-la como pessoa contínua.

Noah interrompe o processo tentando salvar a irmã.

Essa interrupção causa o Silêncio.

Depois, as tentativas de Noah de reconstruir Elia tornam a situação pior. O Coração passa a usar as lembranças dele como modelo para recriá-la.

Noah então escolhe fragmentar as próprias memórias para parar de alimentar essa prisão.

## 2026-08-14 — Tema central

**Decisão:** o jogo fala principalmente sobre **apego, memória, culpa e mudança**.

A frase temática de produção é:

> Preservar algo não é o mesmo que permitir que ele continue vivo.

Essa frase não precisa aparecer literalmente no jogo.

## 2026-08-14 — Catedral Afogada

**Decisão:** primeira região e vertical slice inteira.

Tema: **apego**.

Água é assinatura da região e deve ter função narrativa/visual/mecânica:

- preservar objetos;
- refletir memórias;
- esconder pistas;
- responder a som/impacto;
- reforçar a ideia de algo mantido além da hora de terminar.

Nenhuma segunda região entra em produção antes da Catedral funcionar.

## 2026-08-14 — Estrutura da slice

Ordem V1:

1. Santuário do Último Som;
2. Nave Silenciosa;
3. Galeria dos Quatro Sinos;
4. Claustro Submerso;
5. Câmara do Peso / elite opcional;
6. Corredor sem Eco;
7. Campanário Afogado / Guardião dos Sinos.

Duração-alvo: 10–15 minutos quando completa.

## 2026-08-14 — Motivo de quatro

**Decisão:** um motivo de quatro Pulsos/notas deve reaparecer como linguagem do mundo.

Ele pode ser reconhecido em:

- cartas;
- sinos;
- puzzle;
- ataque de inimigo;
- padrão do chefe;
- memória;
- música.

O jogador atento recebe vantagem de compreensão, não uma seta de tutorial.

## 2026-08-14 — Visual de Noah

**Decisão:** silhueta simples, leve e assimétrica.

Assinaturas:

1. fragmento espectral;
2. pequeno sino dourado;
3. detalhe carmesim curto.

Não usar como base:

- cachecol enorme;
- capa longa;
- armadura pesada;
- excesso de cintos/runas;
- olhos neon permanentes;
- estética cyberpunk;
- protagonista genérico de gacha.

## 2026-08-14 — Engine

**Decisão:** manter **Godot + C#**.

Motivo:

- já é a stack real do repo;
- atende bem o escopo 2D;
- migrar para Unity/Unreal agora gastaria tempo sem validar a mecânica principal.

Alvo atual da branch: Godot 4.7.1 .NET / .NET 8.

## 2026-08-14 — Assets externos

**Decisão:** assets gratuitos/CC0 podem acelerar blockout, mas não definem identidade final.

Noah, inimigos principais, UI, logo, símbolos narrativos e música final devem ser autorais.

Todo asset externo precisa entrar no `ASSET_REGISTRY.md`.

## 2026-08-14 — Estado de validação

O combat toy existe em código, mas ainda **não foi validado por build/playtest no editor Godot 4.7.1 .NET**.

Não tratar feature escrita como feature aprovada.

O primeiro playtest segue `PLAYTEST_01.md`.

## Regra para novas decisões

Quando uma ideia nova for aprovada:

1. registrar aqui;
2. atualizar o documento especialista quando necessário;
3. evitar manter duas versões concorrentes como se fossem igualmente válidas;
4. se for experimento, marcar explicitamente como experimento e não decisão.

## 2026-08-27 — Protagonist moves from human design to an original non-human silhouette

### Changed

- the earlier young-human face, hair and short-coat direction is retired;
- the protagonist now uses a dark non-human face with expressive ivory eye-shapes;
- the costume hierarchy becomes white mantle, black underlayer, crimson scarf and a small collar bell;
- visual detail is reduced to five recognition anchors;
- `Noah` becomes a working name while Senn and Eiro remain candidates.

### Why the prior rule failed

The human concept depended on hair, face and clothing detail to create identity. At the intended sprite scale it risked reading as a generic fantasy protagonist and did not support the stronger mascot-like simplicity requested for the game.

### Preserved

- small silhouette against monumental environments;
- quiet protagonist whose inner life appears through movement and objects;
- crimson, aged gold and spectral blue as meaningful accents;
- bell, Heart fragment and Agulha de Vesper;
- controlled effects and readable combat.

### Player experience gained

The protagonist can now communicate curiosity, playfulness, trust and combat focus through eye animation while remaining recognizable at small scale. The bell also gains a concrete world-facing role: it marks a completed missing pulse instead of making constant decorative noise.

### Naming gate

This gate was closed by the later Noah decision below.

## 2026-08-27 — Noah becomes canonical and gains a white hair silhouette

### Changed

- **Noah** replaces the working-name shortlist across current documentation;
- the no-hair concept is superseded by a compact white hair mass;
- spikes stay relaxed and mostly backward/downward, with two short front fringes;
- the prototype blockout now carries the same readable mass;
- after the first Peregrino falls, approaching the cathedral door produces the first visual bell response.

### Why

The selected name closes an unnecessary identity fork. The hair adds a strong top silhouette and a playful motion channel while the dark non-human face and original eye language remain intact.

### Preserved

- non-human anatomy and dark face;
- original expressive ivory eyes;
- white mantle, black underlayer, crimson scarf and aged-gold bell;
- compact cartoon proportions and controlled visual effects.

### Reference boundary

Outside characters and personal visual references contribute only broad principles such as readable expression, grouped hair mass and motion. Noah's exact eye shapes, hair outline and proportions must remain original.

## 2026-08-27 — Hair gains tension and eye acting enters gameplay

The first white-hair blockout read too relaxed. The crown now rises before breaking backward, the side spikes open more clearly and dash adds a small silhouette flare. It remains a compact original mass rather than reproducing any reference outline.

Eye acting is no longer documentation-only. The combat prototype now distinguishes:

- progressive focus through Cadência;
- a brief confident opening after Perfect timing;
- compressed focus during dash;
- uneven recoil after damage;
- calm symmetry during RÉQUIEM.

These reactions use existing gameplay state, add no new controls and preserve combat readability.

## 2026-08-28 — The first bell response becomes an optional discovery

The environmental response near the cathedral door now has a complete prototype loop:

- after the first Peregrino, the HUD gives a short diegetic hint through Noah's collar bell;
- approaching the door triggers the response once;
- the door keeps a quiet aged-gold memory mark for the rest of the attempt;
- the result panel distinguishes **found** from **hidden**;
- winning without investigating preserves the discovery as replay motivation instead of falsely claiming it happened.

This remains a visual blockout. It does not claim final audio, art or permanent campaign save behavior.

## 2026-08-28 — Bell discovery gains a five-second micro-Echo

The response now unfolds as a short environmental memory instead of stopping at a notification:

1. an aged-gold wave answers Noah's collar bell;
2. two incomplete presences appear only as reflections in the water;
3. three pulses resolve while a fourth remains absent;
4. the phrase **UM PULSO FALTA** closes the sequence.

The second presence is intentionally unreadable: no face, name, costume or relationship is fixed by this blockout. The sequence strengthens the existing mystery without replacing the longer authored Eco planned for the Catedral Afogada.

## 2026-08-28 — Micro-Echo receives a safe focus window

The memory must not punish the player for noticing it. While the five-second sequence is active:

- enemy movement, telegraphs and attacks are suspended;
- session time and Cadência decay are paused;
- cards and dash cannot fire accidentally;
- Noah may still walk and reposition;
- the HUD communicates that movement remains available.

The enemy resumes with a full attack cooldown, preventing an immediate hit on the first frame after the memory. This focus window is scoped to the prototype and still requires playtest before becoming a campaign-wide rule.

## 2026-08-28 — Clamor supersedes the fixed Agulha de Vesper weapon

The fixed narrow blade no longer covers the traversal and expressive range established for Noah. **Clamor** becomes the canonical weapon, built from one coherent visual system in three configurations:

- **Bastão / Repouso** for support, reach and traversal;
- **Lâmina dupla** for arcs and close pressure;
- **Dois bastões** for mobility, sequences and future climbing interactions.

The card named **Agulha** remains a combat action; it is not the weapon's identity. The prototype now lets the player cycle Clamor with `Q`, resets each attempt in Repouso and draws every form with a shared dark core, spectral line and restrained gold junction.

This first integration deliberately changes no damage, range or cooldown. Mechanical differences need build validation and playtest evidence before entering combat balance.

The shift itself receives a short procedural response around Noah's hands: one spectral arc, a restrained gold junction and a connecting line. It is feedback for state change rather than an attack effect, and the HUD position follows viewport width instead of relying on one fixed desktop coordinate.

## 2026-08-28 — Visual cast exploration is separated from campaign presence

Character concepts inspired by people close to the creator may be developed as part of Réquiem's visual world without automatically becoming NPCs in the campaign. The solitude rule remains active until story structure is deliberately revised.

Noah and Anna become the first visual benchmarks. Noah keeps his dark face and ivory eyes; masked supporting-cast studies use light masks with black eye fields, allowing a restrained personal-color reflection. Shared compact proportions, species readability and functional accessories create family resemblance without copying Noah's face or costume.

The local Cast Board tests color, filled silhouette and unchanged 64 px reduction. Other characters enter the board only after their species and two or three signature anchors are actually approved.

## 2026-08-28 — Prototype input is action-based and pause freezes the run

The combat toy no longer owns raw physical-key checks. A small input boundary registers keyboard and gamepad defaults while allowing later project-level remapping to replace them. Keyboard, mouse and controller paths now feed the same actions and rhythm capture.

Pause is local to the prototype rather than the whole SceneTree. It freezes the trial timer, current fallback Pulse clock, enemy, cards and transient effects without stopping unrelated nodes globally. Restart remains available while paused and begins a clean, resumed attempt. When authored music enters the scene, its player must join this boundary explicitly instead of relying on SceneTree pause.

## 2026-08-28 — The first run begins with a briefing; retries do not

The prototype no longer attacks a first-time player while controls are being read. Its initial state presents the concrete objective and both keyboard/controller bindings while arena, enemy, timer and Pulse remain still. Confirming starts all clocks from zero together.

Retries skip this screen and return directly to play. Reset now also clears the combat toy's local Pulse phase, fixing the former mismatch in which the authored shadow timeline restarted but local timing evaluation kept its previous phase.

## 2026-08-28 — Playable-loop contracts receive an executable guard

Input, pause, briefing and synchronized reset depend on ordering across partial classes and the rhythm bridge. A small Python linter now verifies these boundaries before the C# restore/build step. It intentionally checks only high-risk invariants and remains explicit that source validation is not a Godot playtest.

## 2026-08-28 — Anna's mask carries the expression without a pendant

Anna's Cast Board blockout now uses an original angular feline mask with rising black eye fields, sharper cheeks and a restrained crimson center. The theatrical contrast comes from broad visual principles rather than reproducing any referenced character's exact mask or symbol.

The former collar line is removed. Hair mass, mask and grouped tails already provide three readable anchors, so an extra pendant would dilute the hierarchy instead of adding personality.

## 2026-08-28 — The first briefing has one explicit exit

The initial briefing only yields to the confirm action or a primary mouse click. Restart and pause inputs are ignored while it is visible; previously, restart could rebuild the arena without the briefing and silently begin the trial.

This boundary is part of the executable prototype contract so a future input reordering cannot reintroduce the skip.

## 2026-08-28 — The bell micro-Echo freezes both rhythm clocks

The session timer already stopped during the optional memory, but the fallback Pulse phase and authored Echo Trial timeline continued behind it. Returning control could therefore place the player at an unrelated beat.

The micro-Echo now pauses both clocks and resumes them from the same phase. Screen pause composes with this rule: unpausing the overlay cannot restart rhythm while the memory is still active. The contract linter covers both clocks and all three committed-action gates.

## 2026-08-28 — A resolved trial is a frozen state

Victory and defeat previously stopped the session timer but left the local Pulse and authored timeline running beneath the result panel. A resolved trial now freezes the owner loop and rhythm bridge together.

Restart remains the only exit from the result and already rebuilds the arena, local phase, timeline and shadow score atomically. The contract linter now covers this terminal-state boundary.

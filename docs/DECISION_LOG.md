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

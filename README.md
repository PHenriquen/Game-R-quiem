# RÉQUIEM: ECOS DO SILÊNCIO

> **Every death rewrites the music.**

Réquiem é um action roguelite 2D em desenvolvimento. A ideia central é misturar combate em tempo real com uma mão pequena de cartas de ação, exploração solitária e uma narrativa que aparece mais pelo mundo do que por diálogos.

**Noah**, um pequeno ser não humano, acorda em Vesper sem lembrar por que o mundo perdeu parte de sua música. Conforme avança, encontra lugares presos a memórias incompletas e sinais de que o próprio passado está ligado ao Silêncio.

## Duas camadas, um mesmo combate

O projeto passa a separar claramente:

- **Campanha** — experiência autoral e finita; história, Noah, Catedral Afogada, exploração, cartas, inimigos, bosses e narrativa ambiental.
- **Echo Trials** — desafios curtos e replayable construídos sobre música + timeline + precisão + combo + score + ranks.

A influência de rhythm games entra no loop de domínio e replay, não em copiar a interface ou trocar o combate por círculos para clicar. Noah continua se movendo, esquivando e usando cartas em tempo real.

## O que já existe

A base atual tem um **combat toy jogável** em Godot/C# para testar o núcleo antes de investir em arte final:

- movimento livre e esquiva;
- mão de 4 cartas usada em tempo real;
- baralho inicial com 8 cartas;
- Corte Breve, Agulha, Passo Fantasma e Sino Partido;
- Pulso e Cadência de D até RÉQUIEM;
- inimigo simples com ataque telegrafado;
- Prova do Primeiro Eco com objetivo de três confrontos, vitória, derrota, resumo e reinício;
- primeiro segredo opcional: depois do primeiro confronto, o sino de Noah indica uma resposta escondida junto à porta e registra a descoberta no resultado;
- arena e efeitos provisórios desenhados por código;
- relógio musical com compensação de latência.

A foundation de Echo Trials adiciona:

- formato JSON versionado para beatmaps;
- loader e validação;
- `RhythmJudge` independente do combate;
- `ScoreTracker` com accuracy/combo/rank;
- `EchoTrialDirector` para disparar eventos sem acoplar inimigo/arena ao formato;
- primeiro chart de 32 segundos (`Primeiro Eco de Vesper`);
- cena de debug independente para visualizar beat/phase/eventos sem depender de áudio;
- editor visual local em `tools/echo_editor/index.html` para importar, editar e exportar beatmaps;
- `beatmap_seed.py` para gerar uma grade inicial;
- `beatmap_lint.py` rodando no CI.

## Como o combate funciona

As cartas não pausam o jogo. Cada carta executa uma ação imediatamente e depois abre espaço para outra compra.

O Pulso usa três resultados para ações normais:

- **Perfect**
- **Good**
- **Free**

Agir fora do ritmo nunca bloqueia o ataque. Jogar perto do Pulso melhora execução, score e Cadência. **Miss** só entra quando um Echo Trial explicitamente espera uma resposta em um momento do chart.

Quando a Cadência chega a **RÉQUIEM**, Noah entra por alguns segundos num estado de controle maior, como se lembrasse exatamente de como lutava antes.

## Controles do protótipo

| Tecla | Ação |
|---|---|
| `WASD` | mover |
| `Space` | esquivar |
| `1–4` | usar uma carta |
| clique esquerdo | usar a carta clicada |
| `R` | reiniciar a arena |

## Código

Toy atual:

- [`src/prototype/CombatPrototype.cs`](src/prototype/CombatPrototype.cs)
- [`src/prototype/CombatPrototype.Draw.cs`](src/prototype/CombatPrototype.Draw.cs)
- [`src/prototype/CombatPrototype.tscn`](src/prototype/CombatPrototype.tscn)
- [`src/audio/PulseClock.cs`](src/audio/PulseClock.cs)

Rhythm platform:

- [`src/rhythm/BeatmapModels.cs`](src/rhythm/BeatmapModels.cs)
- [`src/rhythm/BeatmapLoader.cs`](src/rhythm/BeatmapLoader.cs)
- [`src/rhythm/RhythmJudge.cs`](src/rhythm/RhythmJudge.cs)
- [`src/rhythm/ScoreTracker.cs`](src/rhythm/ScoreTracker.cs)
- [`src/rhythm/EchoTrialDirector.cs`](src/rhythm/EchoTrialDirector.cs)
- [`src/rhythm/debug/EchoTrialDebug.tscn`](src/rhythm/debug/EchoTrialDebug.tscn)

Stack:

- Godot 4.7.x .NET;
- C# / .NET 8;
- Python para ferramentas offline simples;
- HTML/CSS/JS puro para o editor local de charts.

## Direção do jogo

Réquiem deve passar principalmente:

**solidão ao explorar, curiosidade ao descobrir segredos e domínio durante o combate.**

A primeira região planejada é a **Catedral Afogada**, parcialmente submersa, onde sinos antigos continuam tentando terminar uma sequência que não possui mais um final.

A direção visual é pixel art moderna, personagem pequeno e legível, ambientes maiores que ele e efeitos espectrais usados com moderação. A silhueta não humana de Noah combina cabelo branco compacto e mais energético, rosto escuro com olhos ivory que reagem ao combate, manto branco, camada preta, **cachecol carmesim** e um pequeno sino dourado no colar.

## Rodando localmente

Esta versão ainda precisa do build/playtest confirmado em um editor Godot 4.7.1 .NET.

1. instale Godot 4.7.1 .NET;
2. abra `project.godot`;
3. deixe o Godot restaurar o projeto C#;
4. pressione `F5` para o combat toy atual.

Para testar apenas a nova timeline sem mexer no toy, abra `src/rhythm/debug/EchoTrialDebug.tscn` e execute a cena (`F6`). Ela usa o fallback clock e não precisa da música para mostrar os eventos.

Para editar um Echo Trial visualmente, abra `tools/echo_editor/index.html` no navegador e importe um JSON de `assets/beatmaps/`.

Ferramentas:

```bash
python tools/generate_prototype_audio.py
python tools/beatmap_lint.py
python tools/beatmap_seed.py --bpm 120 --duration 60 --out assets/beatmaps/test.json
```

## Documentação principal

- [`docs/canon/CANON_LOCK.md`](docs/canon/CANON_LOCK.md) — o que a expansão técnica não pode apagar;
- [`docs/RHYTHM_PLATFORM.md`](docs/RHYTHM_PLATFORM.md) — arquitetura de ritmo;
- [`docs/ECHO_TRIALS.md`](docs/ECHO_TRIALS.md) — modo replayable;
- [`docs/CONTENT_PIPELINE.md`](docs/CONTENT_PIPELINE.md) — como escalar conteúdo sem automatizar autoria;
- [`docs/GAME_DESIGN.md`](docs/GAME_DESIGN.md) — estrutura geral;
- [`docs/COMBAT_SPEC.md`](docs/COMBAT_SPEC.md) — regras do toy;
- [`docs/STORY.md`](docs/STORY.md) — narrativa;
- [`docs/ART_BIBLE.md`](docs/ART_BIBLE.md) — direção visual;
- [`docs/AUDIO_DIRECTION.md`](docs/AUDIO_DIRECTION.md) — Pulso e música adaptativa.

## Status

**Pré-produção / protótipo jogável de duelo + rhythm platform foundation.**

A Prova do Primeiro Eco já fecha um loop curto: instruções, três confrontos, condição de vitória/derrota, resumo da tentativa e reinício. O próximo gate continua sendo confirmar o build no Godot 4.7.1 .NET e fazer o primeiro playtest de sensação. Depois disso, o chart autoral pode deixar o modo observacional e passar a dirigir uma sessão própria, com regras calibradas por evidência.

## License

O código-fonte está sob a [MIT License](LICENSE). Nomes, personagens, história, arte, música e demais elementos criativos originais continuam reservados ao autor do projeto.

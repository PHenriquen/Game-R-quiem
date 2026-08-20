# RÉQUIEM: ECOS DO SILÊNCIO

> **Every death rewrites the music.**

Réquiem é um action roguelite 2D em desenvolvimento. A ideia central é misturar combate em tempo real com uma mão pequena de cartas de ação, exploração solitária e uma narrativa que aparece mais pelo mundo do que por diálogos.

Nox acorda em Vesper sem lembrar por que o mundo perdeu parte de sua música. Conforme avança, encontra lugares presos a memórias incompletas e sinais de que o próprio passado está ligado ao Silêncio.

## O que já existe

O projeto ainda está em pré-produção, mas esta branch já tem um **combat toy jogável** feito em Godot/C# para testar o núcleo antes de investir em arte final.

Hoje ele inclui:

- movimento livre e esquiva;
- mão de 4 cartas usada em tempo real;
- baralho inicial com 8 cartas;
- quatro ações: Corte Breve, Agulha, Passo Fantasma e Sino Partido;
- sistema de Pulso a 100 BPM;
- Cadência de D até RÉQUIEM;
- inimigo simples com ataque telegrafado;
- arena e efeitos provisórios desenhados por código;
- gerador de áudio temporário para testar o Pulso e camadas da trilha.

O protótipo visual é propositalmente simples. Quero validar se mover, escolher cartas e acertar o ritmo é divertido antes de transformar isso em uma fase completa.

## Como o combate funciona

As cartas não pausam o jogo. Cada carta executa uma ação imediatamente e depois abre espaço para outra compra.

O Pulso dá três resultados de timing:

- **Perfect**
- **Good**
- **Free**

Errar o ritmo nunca impede o ataque. Jogar perto do Pulso só melhora a execução e ajuda a aumentar a Cadência.

Quando a Cadência chega a **RÉQUIEM**, a ideia não é virar uma transformação cheia de efeitos. Nox entra por alguns segundos num estado de controle maior, como se lembrasse exatamente de como lutava antes.

## Controles do protótipo

| Tecla | Ação |
|---|---|
| `WASD` | mover |
| `Space` | esquivar |
| `1–4` | usar uma carta |
| clique esquerdo | usar a carta clicada |
| `R` | reiniciar a arena |

## Código

Os arquivos principais do toy ficam em:

- [`src/prototype/CombatPrototype.cs`](src/prototype/CombatPrototype.cs) — lógica do protótipo;
- [`src/prototype/CombatPrototype.Draw.cs`](src/prototype/CombatPrototype.Draw.cs) — desenho provisório da arena/HUD;
- [`src/prototype/CombatPrototype.tscn`](src/prototype/CombatPrototype.tscn) — cena do protótipo;
- [`src/audio/PulseClock.cs`](src/audio/PulseClock.cs) — relógio musical que deve substituir o timer provisório quando a trilha real entrar.

Stack atual:

- Godot 4.7.x .NET;
- C# / .NET 8;
- Python apenas para gerar os stems temporários de áudio.

## Direção do jogo

Quero que o Réquiem passe principalmente três sensações:

**solidão ao explorar, curiosidade ao descobrir segredos e domínio durante o combate.**

A primeira região planejada é a **Catedral Afogada**, um lugar parcialmente submerso onde sinos antigos continuam tentando terminar uma sequência que não possui mais um final.

A direção visual é pixel art moderna, personagem pequeno e legível, ambientes maiores que ele e efeitos espectrais usados com moderação. O azul espectral, ouro envelhecido e um detalhe carmesim formam a identidade principal do Nox.

## Rodando localmente

Esta versão ainda precisa do primeiro build/playtest confirmado em um editor Godot 4.7.1 .NET.

Para testar:

1. instale o Godot 4.7.1 .NET;
2. abra `project.godot`;
3. deixe o Godot restaurar o projeto C#;
4. pressione `F5`.

Para gerar os áudios temporários:

```bash
python tools/generate_prototype_audio.py
```

## Documentação

As anotações mais detalhadas ficam em `docs/`. As que mais importam agora são:

- [`GAME_DESIGN.md`](docs/GAME_DESIGN.md) — estrutura geral;
- [`COMBAT_SPEC.md`](docs/COMBAT_SPEC.md) — valores e regras do toy;
- [`STORY.md`](docs/STORY.md) — narrativa sem os principais spoilers;
- [`ART_BIBLE.md`](docs/ART_BIBLE.md) — direção visual;
- [`AUDIO_DIRECTION.md`](docs/AUDIO_DIRECTION.md) — Pulso e música adaptativa.

## Status

**Pré-produção / primeiro combat toy.**

A prioridade agora é confirmar build, jogar o protótipo e ajustar sensação de movimento, cartas, Pulso e Cadência. Depois disso entram arte própria, primeira sala de verdade e inimigos mais completos.

## License

O código-fonte está sob a [MIT License](LICENSE). Nomes, personagens, história, arte, música e demais elementos criativos originais continuam reservados ao autor do projeto.
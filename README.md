# RÉQUIEM: ECOS DO SILÊNCIO

> **Every death rewrites the music.**

Réquiem é um action roguelite 2D em pré-produção. A ideia é combinar combate rápido, uma camada leve de ritmo e cartas de ação sem transformar o jogo num rhythm game tradicional.

Nox acorda em Vesper sem lembrar o que aconteceu com o Coração do Mundo. O cenário, os inimigos e pequenas memórias espalhadas pelo caminho contam a maior parte da história; o protagonista não precisa explicar tudo em diálogos.

## A ideia do combate

O jogador se move e ataca em tempo real. O **Pulso** marca o ritmo da música, mas errar esse timing nunca bloqueia uma ação.

Acertar perto do Pulso melhora a execução e aumenta a **Cadência**:

```text
D → C → B → A → S → RÉQUIEM
```

As cartas representam ações — ataques, movimento ou impacto — e não uma pilha de buffs passivos. Quero que a mão mude as decisões do combate sem parar o jogo a cada poucos segundos.

## Direção atual

O primeiro recorte do jogo está sendo pensado em torno de:

- um único personagem jogável;
- exploração mais solitária;
- uma região principal, a **Catedral Afogada**;
- combate em tempo real com cartas;
- Pulso/Cadência como camada de domínio;
- poucos inimigos bem legíveis;
- segredos e narrativa ambiental;
- pixel art moderna com efeitos usados com moderação.

A prioridade é fechar um protótipo de combate pequeno antes de produzir salas, inimigos e arte em quantidade.

## Nox

Nox é o protagonista e permanece em silêncio durante boa parte da experiência.

A identidade visual planejada usa uma silhueta pequena, roupa escura, um detalhe carmesim, um pequeno sino envelhecido e efeitos azul-espectrais. O ambiente deve chamar mais atenção pelo tamanho e atmosfera do que o personagem por complexidade visual.

## Tecnologia

- Godot 4.x com suporte .NET;
- C# / .NET;
- Windows como primeiro alvo.

O repositório também tem alguns experimentos de telemetria local e orçamento de frame para ajudar no ajuste do jogo. Eles são ferramentas de desenvolvimento, não funcionalidades para o jogador.

## Estrutura

```text
src/       código do jogo e protótipos
docs/      história, combate, arte e decisões de design
assets/    recursos do projeto
tools/     utilitários de desenvolvimento
```

As anotações que mais ajudam a entender a direção atual estão em:

- [`docs/GAME_DESIGN.md`](docs/GAME_DESIGN.md)
- [`docs/STORY.md`](docs/STORY.md)
- [`docs/ART_BIBLE.md`](docs/ART_BIBLE.md)

## Rodando localmente

1. instale uma versão do Godot 4 com suporte .NET;
2. clone o repositório;
3. abra `project.godot`;
4. execute a cena configurada no projeto.

## Status

**Pré-produção.**

Existe uma branch separada onde estou testando o primeiro combat toy. Ela só deve virar a base principal depois de eu confirmar o build no Godot e jogar o protótipo de verdade. Até lá, a `main` representa a direção do projeto, não uma promessa de vertical slice pronta.

## Licença

O código-fonte está sob a [MIT License](LICENSE). Nomes, personagens, história, arte, música e outros elementos criativos originais permanecem reservados ao autor do projeto.

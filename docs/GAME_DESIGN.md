# RÉQUIEM: ECOS DO SILÊNCIO — Game Design Document

> **Título internacional:** REQUIEM: ECHOES OF SILENCE
>
> **Elevator pitch:** roguelite de ação 2D em que acertar ataques e esquivas no ritmo fortalece a build, adiciona camadas à música e transforma cada combate em uma apresentação.

## 1. Visão

**RÉQUIEM: ECOS DO SILÊNCIO** mistura a fluidez e a progressão narrativa de um action roguelite, a melancolia de uma fantasia em ruínas, a liberdade de builds de um card game e a precisão viciante de um jogo rítmico.

O jogo não exige que todas as ações sejam feitas no ritmo. O combate continua responsivo fora do pulso; jogar no tempo correto oferece domínio, eficiência e espetáculo.

**Promessa ao jogador:** “Eu começo sobrevivendo ao ritmo e termino comandando a música.”

## 2. Mundo e narrativa

Vesper era mantida viva pelo Coração do Mundo, uma máquina colossal que regulava matéria, memória e tempo por meio de música. Quando o Coração se partiu, regiões inteiras foram congeladas em um silêncio sobrenatural. Seus habitantes perderam a própria cadência e se tornaram Dissonantes.

Nox desperta no Santuário do Último Som com um fragmento pulsando no peito. Ele atravessa Vesper para reunir os fragmentos restantes, enquanto descobre que cada morte reinicia uma versão imperfeita da última canção do mundo.

### Nox

- silhueta escura e pequena;
- máscara de marfim rachada;
- capa curta e cachecol carmesim reativo;
- fragmento azul-espectral no peito;
- identidade reconstruída por ações e memórias.

O tom é melancólico, misterioso e elegante, mas não completamente desesperançoso. O Santuário traz calor, humor contido e relações que avançam após cada run.

## 3. Loop principal

1. Despertar no Santuário.
2. Conversar e investir recursos permanentes.
3. Escolher arma, lembrança e cartas iniciais.
4. Selecionar uma rota.
5. Vencer combate, evento ou desafio de precisão.
6. Escolher carta, melhoria ou recurso.
7. Reorganizar a Partitura.
8. Enfrentar miniboss e maestro da região.
9. Retornar após morrer ou concluir a área.

Uma run completa deve durar de 20 a 35 minutos. A vertical slice terá aproximadamente 15 minutos.

## 4. Combate e Pulso

Ações básicas: ataque leve, ataque especial da arma, esquiva, aparo quando permitido e Clímax — uma habilidade suprema carregada durante o combate.

Um relógio musical central publica eventos de pulso. Jogador, inimigos, efeitos e trilha consultam a mesma fonte de tempo.

| Resultado | Distância do pulso | Efeito inicial |
|---|---:|---|
| Perfeito | até 55 ms | bônus completo e +2 Cadência |
| Bom | até 110 ms | bônus parcial e +1 Cadência |
| Livre | acima de 110 ms | ação normal, sem punição |
| Dissonante | erros repetidos | redução temporária de ganho |

Esses valores são hipóteses de protótipo e devem ser validados por testes.

## 5. Cadência

**D → C → B → A → S → REQUIEM**

Cada nível adiciona uma camada à trilha, intensifica efeitos, altera o cenário, melhora recursos e habilita cartas. Receber dano reduz parte do medidor; um erro isolado não destrói a sequência.

## 6. Cartas e Partitura

Cartas são técnicas passivas, modificadores e gatilhos equipados. Elas não são compradas durante a luta.

- até 8 cartas equipadas;
- melhoria ou fusão durante a run;
- 3 cartas da mesma família criam uma Harmonia;
- famílias combinadas criam Acordes híbridos.

### Famílias

- **Sangue:** risco, dano, sacrifício e recuperação.
- **Véu:** movimento, clones, velocidade e esquiva.
- **Sino:** defesa, impacto, atordoamento e contra-ataque.
- **Túmulo:** maldições, espíritos, execução e controle.
- **Ruído:** alteração de BPM e grande poder com instabilidade.

### Exemplos

- **Último Compasso:** o quarto ataque perfeito explode.
- **Passo Fantasma:** esquiva perfeita deixa um clone que repete o último golpe.
- **Dívida de Sangue:** aumenta dano, mas erros consecutivos consomem vida.
- **Sino Partido:** aparos perfeitos criam uma onda de impacto.
- **Nota Morta:** derrotados deixam uma nota que cura ou explode.
- **Contratempo:** ações no pulso secundário contam, mas a janela é menor.

## 7. Armas

- **Agulha de Vesper:** lâmina fina e rápida conectada a fios espectrais.
- **Carrilhão:** martelo-sino pesado que cria ondas circulares.
- **Arco de Cordas:** arma de médio alcance que acumula sequências de notas.

A vertical slice usa apenas a Agulha de Vesper.

## 8. Regiões

- **Catedral Afogada:** água negra, arcos quebrados e sinos submersos.
- **Teatro das Marionetes:** fios, bonecos e ataques em contratempo.
- **Jardim sem Vento:** plantas detectam vibrações e deslocamento.
- **Torre do Maestro:** BPM variável e combinação de padrões anteriores.

## 9. Chefes

- **Guardião dos Sinos:** ondas circulares lentas e impacto pesado.
- **Cantoras Gêmeas:** uma ataca no pulso, outra no contratempo.
- **Marionetista:** interfere temporariamente nas cartas.
- **Maestro Pálido:** altera BPM e compasso.
- **O Silêncio:** remove a trilha e exige leitura visual.

A vertical slice termina no Guardião dos Sinos.

## 10. Identidade visual e sonora

Paleta: preto azulado `#090B12`, marfim `#E9E2D0`, carmesim `#9E1738`, azul espectral `#54C7CE` e dourado envelhecido `#C4A35A`.

Cenários escuros e texturizados contrastam com personagens de formas simples e efeitos geométricos brilhantes. Ataques inimigos nunca dependem apenas de cor.

A trilha combina piano, cordas, coral, sinos, bateria eletrônica e máquinas processadas. Cada faixa é dividida em ambiente, harmonia, percussão, melodia e clímax. A Cadência mistura essas camadas sem reiniciar a música.

## 11. Progressão

Durante a run: cartas, melhorias de arma, moeda temporária e rotas.

Permanente: novas cartas no conjunto de recompensas, lembranças iniciais, armas, fragmentos narrativos e opções no Santuário. Melhorias puramente numéricas serão limitadas.

## 12. Acessibilidade

- calibração manual e automática de latência;
- Pulso visual, sonoro e tátil opcional;
- tamanho e intensidade ajustáveis;
- alto contraste e remapeamento completo;
- assistência de ritmo com janela ampliada;
- redução de flashes e tremor de câmera;
- nenhuma informação essencial apenas por áudio.

## 13. Vertical slice

Nox, Agulha de Vesper, Catedral Afogada, 24 cartas, 4 inimigos, 1 miniboss, Guardião dos Sinos, 8–10 salas, salvamento, controles, uma trilha adaptativa e narrativa curta.

### Fora do primeiro escopo

Multiplayer, mobile, dublagem completa, múltiplas regiões, dezenas de armas e procedural irrestrito.

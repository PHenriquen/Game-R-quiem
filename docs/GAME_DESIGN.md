# RÉQUIEM: ECOS DO SILÊNCIO — Game Design Document

> **Título internacional:** REQUIEM: ECHOES OF SILENCE
>
> **Elevator pitch:** roguelite de ação 2D em que acertar ataques e esquivas no ritmo fortalece a build e transforma gradualmente a música da run.

## 1. Visão

**RÉQUIEM: ECOS DO SILÊNCIO** mistura combate de ação simples, uma camada rítmica opcional e escolhas de cartas que mudam a forma de jogar.

O projeto deve permanecer pequeno o suficiente para ser concluído. A primeira versão não tenta competir em quantidade de conteúdo com roguelites grandes; ela precisa entregar **uma ideia central forte, uma área memorável e um combate gostoso**.

**Promessa ao jogador:** “Eu começo sobrevivendo ao ritmo e termino comandando a música.”

## 2. Mundo e narrativa

Vesper era mantida viva pelo **Coração do Mundo**, uma máquina antiga capaz de estabilizar matéria, memória e tempo por meio de padrões sonoros. Quando o Coração se partiu, uma parte do mundo perdeu não apenas o som, mas a própria capacidade de manter suas memórias inteiras.

As pessoas afetadas se tornaram **Dissonantes**: corpos ainda presentes, mas presos a fragmentos repetidos do que foram.

Nox desperta no **Santuário do Último Som** com um fragmento do Coração pulsando no peito. Ele não lembra quem era. A cada run, atravessa a Catedral Afogada e recupera Ecos — memórias que podem pertencer a ele, aos Dissonantes ou ao próprio mundo.

A verdade central da primeira versão é simples: Nox não está apenas tentando reparar Vesper. O fragmento em seu peito faz parte da causa do colapso, e cada morte reinicia a tentativa incompleta de terminar o último Réquiem.

O tom é melancólico e misterioso, mas não niilista. O Santuário representa calor, humanidade e progresso.

## 3. Nox

Nox é o único personagem jogável da primeira versão.

### Silhueta

- jovem de porte leve;
- cabelo escuro irregular;
- sobretudo curto azul-negro;
- cachecol carmesim longo o bastante para comunicar movimento;
- pequeno sino dourado preso ao peito/cinto;
- fragmento azul-espectral visível próximo ao coração;
- espada estreita de energia espectral.

A máscara de marfim deixa de ser peça permanente do rosto. Ela aparece como **motivo narrativo e visual** em lembranças, alta Cadência e materiais promocionais, preservando a identidade sem esconder o protagonista o tempo todo.

### Personalidade

Nox é observador, reservado e curioso. Ele não precisa falar muito. A personalidade aparece em escolhas curtas, reações no Santuário e pequenos fragmentos de memória.

## 4. Loop principal

1. Acordar no Santuário.
2. Conversar com 2–3 personagens.
3. Escolher cartas iniciais.
4. Entrar na Catedral Afogada.
5. Vencer sala curta de combate.
6. Escolher 1 de 3 recompensas.
7. Reorganizar a Partitura.
8. Enfrentar um evento ou elite.
9. Enfrentar o Guardião dos Sinos.
10. Voltar ao Santuário após vitória ou morte.

**Duração-alvo da primeira versão: 10–15 minutos por run.**

## 5. Combate e Pulso

Ações da primeira versão:

- ataque;
- combo simples;
- esquiva;
- ataque especial da arma;
- Clímax quando o medidor enche.

Sem aparo obrigatório na primeira versão.

Um relógio musical central publica eventos de Pulso. Jogador, inimigos, VFX e trilha consultam a mesma fonte.

| Resultado | Distância do pulso | Efeito inicial |
|---|---:|---|
| Perfeito | até 60 ms | bônus completo e +2 Cadência |
| Bom | até 125 ms | bônus parcial e +1 Cadência |
| Livre | acima de 125 ms | ação normal, sem punição |

O jogador **nunca perde o comando porque errou o ritmo**. O ritmo melhora a ação; não substitui o action game.

## 6. Cadência

**D → C → B → A → S → REQUIEM**

A Cadência sobe com ações bem sincronizadas e cai ao receber dano ou cometer muitos erros consecutivos.

Na primeira versão, cada faixa deve fazer somente três coisas:

- adicionar/intensificar uma camada musical;
- aumentar discretamente feedback visual;
- ativar efeitos de algumas cartas.

Nada de criar dezenas de bônus globais difíceis de balancear.

## 7. Cartas e Partitura

Para manter o jogo simples, a vertical slice usa **12 cartas**, não 24.

A Partitura comporta até **6 cartas**.

Três famílias iniciais:

### Sangue — carmesim
Dano, risco e recuperação.

### Véu — violeta
Movimento, esquiva e velocidade.

### Sino — dourado envelhecido
Impacto, defesa e ondas de choque.

Famílias Túmulo e Ruído ficam reservadas para expansão futura.

### Exemplos

- **Último Compasso:** o quarto ataque Perfeito causa uma explosão curta.
- **Passo Fantasma:** esquiva Perfeita deixa um eco que repete parte do último golpe.
- **Dívida de Sangue:** aumenta dano enquanto a Cadência estiver em A ou superior.
- **Sino Partido:** derrotar inimigo com ataque Perfeito gera onda de impacto.
- **Fio Carmesim:** combo final puxa levemente inimigos próximos.
- **Respiração do Véu:** três esquivas boas/perfeitas reduzem brevemente o cooldown do especial.

As cartas precisam ser compreensíveis em uma frase.

## 8. Arma

A primeira versão possui apenas uma arma:

### Agulha de Vesper

Uma lâmina estreita conectada ao fragmento do peito de Nox.

- combo rápido de três golpes;
- especial: corte linear de médio alcance;
- Clímax: sequência curta sincronizada com quatro pulsos fortes da música.

Novas armas só entram depois que essa base estiver divertida.

## 9. Área: Catedral Afogada

A primeira versão inteira acontece aqui.

Visual:

- arquitetura gótica parcialmente submersa;
- pedra azul-escura;
- sinos quebrados;
- água refletindo luz espectral;
- velas e lanternas douradas como pontos de calor;
- vegetação violeta discreta;
- ruínas de uma cidade ao redor.

Estrutura da run:

- 5–6 salas principais;
- 3 inimigos comuns;
- 1 elite opcional;
- 1 chefe.

Não haverá geração procedural complexa na primeira versão. Salas autorais podem ser combinadas em pequenas variações de ordem.

## 10. Inimigos

### Peregrino Oco
Melee simples, aproxima e golpeia no pulso principal.

### Cantor Partido
Projéteis lentos em padrões musicais fáceis de ler.

### Guardião Afogado
Inimigo pesado, curto alcance e ondas no chão.

### Elite: Sineiro Sem Rosto
Mistura impacto e pequenas mudanças no tempo dos ataques.

## 11. Chefe — Guardião dos Sinos

O chefe da primeira versão.

Três padrões principais:

1. onda circular;
2. golpe pesado com atraso musical;
3. sequência de sinos que cria zonas seguras/perigosas.

Na metade da vida, a música ganha percussão e o cenário começa a responder visualmente.

O chefe não precisa ter muitas fases. Precisa ser legível, bonito e satisfatório.

## 12. Santuário do Último Som

Hub pequeno e acolhedor.

Elementos da primeira versão:

- Nox;
- uma guardiã do Santuário;
- um artesão/restaurador;
- altar do Coração;
- seleção de cartas;
- mural de Ecos recuperados.

Nada de cidade gigante ou dezenas de NPCs agora.

## 13. História da primeira versão

Estrutura em cinco batidas:

1. **Despertar:** Nox acorda sem memória e ouve um único sino.
2. **Primeiro Eco:** descobre que alguns Dissonantes repetem memórias de pessoas reais.
3. **Dúvida:** um Eco reconhece Nox antes de desaparecer.
4. **Guardião:** o chefe protege um fragmento que reage ao peito de Nox.
5. **Revelação curta:** Nox vê a própria mão tocando o Coração do Mundo antes da ruptura.

A vertical slice termina aí. A pergunta para continuar é: **Nox tentou destruir o Coração ou salvá-lo?**

## 14. Identidade visual

Direção: **pixel art moderna de fantasia melancólica**, com leitura clara e iluminação atmosférica.

Referências de qualidade são usadas por princípio, não para cópia:

- clareza e escala acessível de action RPGs 2D;
- atmosfera de mundo em ruínas;
- iluminação e partículas modernas sobre pixel art;
- HUD limpo e pouco intrusivo.

Paleta:

- Night black `#090B12`
- Ivory `#E9E2D0`
- Crimson `#9E1738`
- Spectral blue `#54C7CE`
- Aged gold `#C4A35A`
- Veil violet `#6651A6`

### Regra visual

Cenário pode ser detalhado; gameplay precisa ser legível.

- inimigos possuem silhuetas claras;
- ataques perigosos usam forma + movimento, não somente cor;
- efeitos do jogador são azul-espectrais;
- carmesim é reservado a Nox/Sangue e momentos narrativos;
- dourado indica calor, Santuário, sinos e memória preservada.

## 15. Som e música

Uma faixa adaptativa principal para a Catedral.

Camadas:

1. ambiente;
2. cordas/piano;
3. percussão;
4. sinos;
5. clímax.

A Cadência mistura essas camadas sem reiniciar a música.

A primeira versão não precisa de uma trilha enorme; **uma música excelente e adaptativa vale mais que dez medianas**.

## 16. Progressão

Durante a run:

- cartas;
- pequenas melhorias;
- Ecos temporários.

Entre runs:

- desbloqueio gradual das 12 cartas;
- fragmentos narrativos;
- pequenas opções iniciais.

Evitar grind numérico pesado.

## 17. Acessibilidade

- calibração de latência;
- Pulso visual e sonoro;
- assistência de ritmo;
- remapeamento;
- redução de flashes e câmera;
- nenhuma informação essencial somente por áudio.

## 18. Vertical slice fechada

Primeira meta realmente jogável:

- Nox;
- Agulha de Vesper;
- Santuário simples;
- Catedral Afogada;
- 12 cartas;
- 3 famílias;
- 3 inimigos;
- 1 elite;
- Guardião dos Sinos;
- 5–6 salas;
- 10–15 minutos;
- uma faixa musical adaptativa;
- salvamento básico;
- teclado + controle.

### Fora do primeiro escopo

- multiplayer;
- mobile;
- dublagem;
- múltiplos personagens;
- mais de uma área completa;
- mais de uma arma;
- procedural avançado;
- dezenas de NPCs;
- dezenas de chefes;
- árvores de habilidade extensas.

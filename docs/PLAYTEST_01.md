# RÉQUIEM — Playtest 01: Combat Toy

> Objetivo: descobrir se o núcleo é divertido antes de polir arte, lore ou conteúdo.

## Preparação

- Godot: **4.7.1 .NET**
- Branch: `work/vertical-slice-v2`
- Cena principal: `src/prototype/CombatPrototype.tscn`
- Resolução-alvo: 1280×720

Controles atuais:

- `WASD` — mover;
- `Espaço` — esquiva universal;
- `1–4` — usar carta;
- clique — usar carta;
- `R` — reiniciar arena.

## Passo 0 — Compilação

Antes de avaliar gameplay:

- [ ] projeto abriu no Godot 4.7.1 .NET;
- [ ] restore do NuGet terminou;
- [ ] C# compilou sem erro;
- [ ] cena abriu com F5;
- [ ] não há erros vermelhos no Output.

Se falhar, copiar **a primeira mensagem de erro completa** e corrigir antes de avaliar sensação.

---

## Teste A — Movimento, sem pensar nas cartas

Jogar 30–60 s apenas se movimentando e desviando do Peregrino.

Avaliar de 1 a 5:

- resposta ao apertar/soltar direção: `__/5`;
- mudança de direção: `__/5`;
- velocidade: `__/5`;
- distância da esquiva: `__/5`;
- sensação geral de controle: `__/5`.

Marcar se acontecer:

- [ ] parece escorregadio;
- [ ] parece lento;
- [ ] parece rápido demais;
- [ ] esquiva curta demais;
- [ ] esquiva longa demais;
- [ ] personagem parece preso durante ações.

Comentário curto:

`__________________________________________________`

---

## Teste B — Identidade das quatro ações

Usar cada ação várias vezes sem se preocupar com timing.

### Corte Breve

Deve parecer: **rápido, confiável, ligação**.

Nota: `__/5`

Problema principal:

`__________________________________________________`

### Agulha

Deve parecer: **precisa, linear, segura a médio alcance**.

Nota: `__/5`

Problema principal:

`__________________________________________________`

### Passo Fantasma

Deve parecer: **movimento ofensivo, não segunda esquiva gratuita**.

Nota: `__/5`

Problema principal:

`__________________________________________________`

### Sino Partido

Deve parecer: **pesado pelo timing e impacto, não por explosão visual**.

Nota: `__/5`

Problema principal:

`__________________________________________________`

### Pergunta decisiva

Se eu esconder o nome das cartas, consigo reconhecer qual usei só pelo comportamento?

- [ ] sim;
- [ ] mais ou menos;
- [ ] não.

---

## Teste C — A mão realmente muda decisões?

Jogar por 2–3 minutos usando o que aparecer.

Observar:

- [ ] às vezes mudei meu plano por causa das quatro cartas disponíveis;
- [ ] fiquei olhando demais para a HUD;
- [ ] apenas apertei qualquer carta disponível;
- [ ] fiquei esperando sempre a mesma carta;
- [ ] o redraw criou ritmo interessante;
- [ ] o redraw pareceu punição/espera.

Pergunta principal:

**A mão cria decisão ou só troca qual botão de ataque está disponível?**

Resposta:

`__________________________________________________`

Se a resposta for “só troca botão”, não adicionar mais cartas. Corrigir o núcleo primeiro.

---

## Teste D — Pulso

Primeiro jogar **ignorando completamente** o círculo de Pulso por 30 s.

O combate ainda precisa funcionar.

- [ ] sim;
- [ ] não.

Depois tentar acertar Bom/Perfeito por 1–2 min.

Avaliar:

- timing fácil demais: `sim / não`;
- timing difícil demais: `sim / não`;
- Perfeito é perceptível: `sim / mais ou menos / não`;
- errar o ritmo estraga o combate: `sim / não`.

Nota geral do Pulso: `__/5`

Regra: se jogar fora do ritmo parecer “errado”, o sistema está rígido demais.

---

## Teste E — Cadência e aura

Tentar chegar a A, S e RÉQUIEM.

Cronometrar aproximadamente:

- D → A: `____ s`;
- D → S: `____ s`;
- D → RÉQUIEM: `____ s`.

Avaliar:

- [ ] subir rank dá vontade de continuar jogando bem;
- [ ] a barra sobe rápido demais;
- [ ] sobe devagar demais;
- [ ] tomar dano destrói momentum demais;
- [ ] RÉQUIEM é perceptível;
- [ ] RÉQUIEM parece só “mais brilho”;
- [ ] RÉQUIEM realmente passa sensação de controle/presença.

Nota do momento RÉQUIEM: `__/5`

---

## Teste F — Peregrino Oco

Jogar contra o inimigo várias vezes.

- telegraph dá para entender: `__/5`;
- tempo de reação justo: `__/5`;
- pressão sobre o jogador: `__/5`;
- movimentação do inimigo: `__/5`;
- luta repetida ainda ajuda a testar cartas: `__/5`.

Marcar:

- [ ] ataque impossível de ler;
- [ ] ataque fácil demais;
- [ ] inimigo passa tempo demais andando;
- [ ] inimigo cola demais em Nox;
- [ ] esquiva responde bem ao ataque;
- [ ] dano recebido parece claro.

---

## Teste G — “30 segundos de Réquiem”

Jogar normalmente por 30 segundos e responder sem pensar muito:

**O que mais parece diferente de outro action roguelite?**

`__________________________________________________`

**O que mais parece genérico?**

`__________________________________________________`

**Qual foi o momento mais legal?**

`__________________________________________________`

**Qual foi o momento mais irritante?**

`__________________________________________________`

Esta parte vale mais que uma nota média.

---

## Resultado

Notas rápidas:

| Área | Nota |
|---|---:|
| Movimento | `__/5` |
| Cartas | `__/5` |
| Mão/decisão | `__/5` |
| Pulso | `__/5` |
| Cadência/RÉQUIEM | `__/5` |
| Inimigo | `__/5` |
| Identidade | `__/5` |

### Top 3 problemas

1. `_______________________________________________`
2. `_______________________________________________`
3. `_______________________________________________`

### Top 3 coisas que já funcionam

1. `_______________________________________________`
2. `_______________________________________________`
3. `_______________________________________________`

## Ordem de correção depois do teste

1. crash/erro de compilação;
2. input/movimento;
3. leitura das quatro cartas;
4. hit feel;
5. mão e redraw;
6. Pulso;
7. Cadência;
8. inimigo;
9. HUD;
10. só então arte/ambiente mais caro.

## Gaps conhecidos antes do Playtest 01

Estes pontos são conscientes e **não** devem ser confundidos com feature final:

- o toy ainda usa formas primitivas, não sprite final de Nox;
- não há trilha adaptativa ligada à cena;
- `PulseClock` de produção existe, mas o toy ainda usa seu relógio provisório interno;
- efeitos de Eco de algumas cartas ainda são simplificados;
- stagger real ainda não está implementado;
- Frase/combo contextual ainda não está implementado no toy;
- não existe câmera final/shake refinado;
- apenas um inimigo existe ao mesmo tempo;
- telemetria V2 ainda precisa ser integrada ao toy.

Esses gaps só viram prioridade depois de confirmar que o núcleo básico abre e responde bem.

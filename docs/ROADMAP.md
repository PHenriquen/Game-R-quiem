# RÉQUIEM — Roadmap V2

O roadmap prova a sensação do jogo antes de aumentar conteúdo.

## M0 — Direção e fundação

- [x] Nome e premissa-base
- [x] Repositório e documentação inicial
- [x] Godot + C#
- [x] Direção criativa V2
- [x] Regra de solidão / elenco reduzido
- [x] Cartas redefinidas como ações em tempo real
- [x] Especificação inicial de combate
- [x] Protótipo de arena sem assets finais
- [ ] Validar compilação no Godot 4.7.1
- [ ] Guia visual final de Noah
- [ ] Protótipo sonoro original

**Saída:** projeto abre direto em uma arena funcional e a direção não possui contradições principais.

## M1 — Combat toy

- [x] Movimento provisório
- [x] Evasão universal provisória
- [x] Mão de 4 cartas
- [x] Baralho inicial de 8 cartas
- [x] Corte Breve
- [x] Agulha
- [x] Passo Fantasma
- [x] Sino Partido
- [x] Pulso provisório a 100 BPM
- [x] Cadência D–RÉQUIEM provisória
- [x] Peregrino Oco provisório
- [ ] Corrigir problemas encontrados no primeiro playtest
- [ ] Transformar valores mágicos em dados editáveis
- [ ] Separar relógio de Pulso da cena
- [ ] Separar mão/baralho da cena
- [ ] FSM simples de Noah
- [ ] hitbox/hurtbox de produção
- [ ] suporte inicial a controle

**Saída:** jogar por 3 minutos é divertido usando apenas formas provisórias.

## M2 — Hit feel + som

- [ ] Primeiro sprite/animatic de Noah
- [ ] poses-chave das quatro ações
- [ ] hit-stop refinado
- [ ] câmera leve
- [ ] telegraphs por forma e movimento
- [ ] primeira camada ambiente
- [ ] metrônomo/Pulso sincronizado a áudio real
- [ ] stems de Cadência
- [ ] camada RÉQUIEM
- [ ] calibração inicial de latência

**Saída:** subir de D para RÉQUIEM é claramente perceptível com olhos e ouvidos.

## M3 — Nave Silenciosa

- [ ] blockout autoral da sala
- [ ] tiles temporários ou CC0 documentados
- [ ] água / reflexos provisórios
- [ ] sino interativo
- [ ] porta/foco visual
- [ ] Peregrino Oco final do slice
- [ ] Cantor Partido
- [ ] primeiro segredo mecânico
- [ ] primeiro micro-Eco

**Saída:** uma sala já transmite exploração, combate e mistério de Réquiem.

## M4 — Noah e identidade visual

- [ ] concept sheet de Noah
- [ ] silhuetas alternativas
- [ ] paleta fechada
- [ ] sprite-base final
- [ ] idle / walk / dash
- [ ] quatro ações-base
- [ ] estado RÉQUIEM
- [ ] ícones das quatro cartas
- [ ] frame visual das cartas
- [ ] linguagem visual dos Dissonantes

**Saída:** screenshot sem logo já parece do mesmo jogo.

## M5 — Catedral curta

- [ ] Santuário vazio e reativo
- [ ] 5–6 salas autorais
- [ ] terceiro inimigo
- [ ] elite opcional
- [ ] recompensas simples
- [ ] transições entre salas
- [ ] morte e retorno
- [ ] Ecos persistentes
- [ ] save básico

**Saída:** run completa sem chefe funciona do início ao retorno.

## M6 — Guardião dos Sinos

- [ ] design visual
- [ ] três padrões centrais
- [ ] sequência de sinos antecipada em segredo/ambiente
- [ ] segunda metade musical
- [ ] memória final do slice
- [ ] balanceamento

**Saída:** 10–15 minutos culminam em um chefe que testa o que o jogo ensinou.

## M7 — Vertical slice pública

- [ ] 8–12 cartas finais para o slice
- [ ] mix de áudio consistente
- [ ] acessibilidade básica
- [ ] teclado + controle
- [ ] performance estável
- [ ] telemetria local revisada
- [ ] opções e pausa
- [ ] build Windows
- [ ] sessão de playtest externa
- [ ] correções
- [ ] trailer curto
- [ ] página itch.io atualizada

**Saída:** build que pode ser entregue a alguém sem precisar explicar como imaginar o jogo pronto.

## Regras de produção

1. Não criar outra região antes de fechar a Catedral.
2. Não criar dezenas de cartas antes de as quatro ações-base funcionarem.
3. Nenhum asset temporário pode definir a identidade final por acidente.
4. Todo asset externo precisa de fonte/licença documentada.
5. Dados de gameplay saem da lógica principal assim que o protótipo provar a mecânica.
6. Toda milestone termina com algo executável ou verificável.
7. Bugs de sensação têm prioridade sobre conteúdo novo.
8. Se uma feature prejudicar solidão, leitura ou impacto, ela precisa justificar a existência.

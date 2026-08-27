# RÉQUIEM — Especificação do Protótipo de Combate

## Objetivo

Validar em uma arena simples a pergunta:

**movimento livre + cartas como ações + Pulso + Cadência consegue ser divertido por três minutos?**

Sem arte final, progressão ou narrativa obrigatória.

## 1. Controles do protótipo

### Movimento

- `WASD` / setas: mover Noah;
- `Espaço`: Passo (evasão universal);
- `1–4`: usar a carta do respectivo espaço da mão;
- `R`: reiniciar arena.

A evasão fica fora do baralho para impedir situações em que uma mão ruim torne dano inevitável.

## 2. Mão e baralho

### Mão

4 espaços sempre visíveis na parte inferior.

Ao usar uma carta:

1. a ação inicia imediatamente;
2. a carta é enviada ao descarte;
3. após o tempo de compra, a próxima carta entra naquele espaço;
4. quando o baralho acaba, o descarte é embaralhado novamente.

### Protótipo

Baralho de 8 cartas, duas cópias de cada ação-base:

- 2× Corte Breve;
- 2× Agulha;
- 2× Passo Fantasma;
- 2× Sino Partido.

Não existe raridade no primeiro protótipo.

## 3. Ações iniciais

### Corte Breve

**Função:** ataque seguro / ligação de combo.

- alcance: 82 px;
- arco frontal amplo;
- dano: 14;
- recuperação: 0,22 s;
- compra: 0,20 s;
- Bom: +10% de alcance;
- Perfeito: +20% de dano e hit-stop ligeiramente maior.

Sensação: corte seco, rápido, quase sem partículas.

### Agulha

**Função:** pressão linear / manter fluxo.

- alcance: 230 px;
- largura: 26 px;
- dano: 11;
- recuperação: 0,28 s;
- compra: 0,25 s;
- Bom: atravessa o primeiro alvo;
- Perfeito: deixa um Eco que repete 45% do dano após 0,12 s.

Sensação: linha espectral fina seguida pelo impacto.

### Passo Fantasma

**Função:** reposicionar atacando.

- deslocamento: 145 px;
- duração: 0,16 s;
- dano no caminho: 9;
- invulnerabilidade ofensiva: 0,10 s;
- recuperação: 0,32 s;
- Bom: +15 px de deslocamento;
- Perfeito: cria um Eco no ponto de partida que executa um Corte Breve fraco.

Sensação: Noah some apenas o suficiente para o olho sentir ausência; evitar teleporte chamativo.

### Sino Partido

**Função:** peso / interrupção / finalizador curto.

- raio: 105 px;
- dano: 24;
- preparação: 0,24 s;
- recuperação: 0,48 s;
- Bom: stagger maior;
- Perfeito: onda secundária de 40% do dano após 0,14 s.

Sensação: pequeno silêncio antes do impacto, câmera curta e uma onda dourada controlada.

## 4. Evasão universal — Passo

- distância: 105 px;
- duração: 0,15 s;
- invulnerabilidade: 0,12 s;
- cooldown: 0,65 s;
- não causa dano;
- pode cancelar o final de uma recuperação leve, mas não o início do Sino Partido.

Uma evasão feita dentro da janela de Pulso pode gerar Cadência, mas não recebe bônus de distância.

## 5. Pulso

BPM inicial: **100**.

1 beat = 0,60 s.

Janelas iniciais:

- Perfeito: até ±65 ms;
- Bom: até ±140 ms;
- Livre: fora dessas janelas.

Nenhuma ação é recusada por timing ruim.

### Feedback

Livre:

- efeito normal;
- sem texto grande.

Bom:

- borda da carta pulsa;
- microflash no fragmento;
- +4 Cadência.

Perfeito:

- ataque recebe propriedade especial;
- som/impacto mais limpo;
- +8 Cadência.

## 6. Cadência

Valor interno: `0–100`.

Faixas:

- D: 0–14;
- C: 15–29;
- B: 30–49;
- A: 50–69;
- S: 70–89;
- RÉQUIEM: 90–100.

Ganhos iniciais:

- ação Boa: +4;
- ação Perfeita: +8;
- derrotar inimigo: +5;
- esquiva perfeita contra golpe real: +7 (fase posterior).

Perdas:

- receber dano: -20;
- 2,5 s sem ação ofensiva: inicia queda de 5/s;
- errar ataque não remove Cadência instantaneamente.

## 7. Estado RÉQUIEM

Ao atingir 90 pela primeira vez:

- trava queda de Cadência por 6 s;
- tempo de compra de cartas -20%;
- todo terceiro ataque deixa um Eco de 35% do dano;
- Noah recebe mudança de postura/outline/fissuras;
- camada musical final entra;
- efeitos ficam mais precisos, não maiores.

Depois de 6 s, o valor pode voltar a cair normalmente.

## 8. Combo

Não existe contador de combo obrigatório para dano.

Existe uma **Frase**, sequência curta das últimas ações.

O protótipo guarda as últimas 4 cartas usadas.

Algumas sequências futuras podem gerar propriedades emergentes, mas a V1 não precisa decorar receitas.

Primeira interação a testar:

`Corte Breve → Passo Fantasma → Sino Partido`

Se as três ações forem Boas ou melhores, o Sino gera uma onda espectral adicional sem dano, apenas para feedback de domínio.

Isso testa se combinações contextuais são prazerosas sem criar um sistema de fighting game escondido.

## 9. Inimigo 01 — Peregrino Oco

Objetivo: validar leitura e pressão.

- vida: 70;
- velocidade: 105 px/s;
- mantém distância de 95–120 px antes de atacar;
- telegraph: 0,48 s;
- golpe: arco frontal;
- dano: 18;
- recuperação: 0,75 s.

Regra: o ataque precisa ser percebido pela pose/forma mesmo sem cor.

## 10. Inimigo 02 — Cantor Partido

Só entra depois de o primeiro duelo estar bom.

- vida: 45;
- mantém distância;
- dispara projétil lento sincronizado a cada 2 beats;
- padrões simples que forçam reposicionamento.

## 11. Hit feel

Valores iniciais para teste:

- Corte: hit-stop 35 ms;
- Agulha: 25 ms;
- Passo Fantasma: 25 ms;
- Sino Partido: 70 ms;
- camera shake máximo do protótipo: 3–6 px;
- flashes abaixo de 80 ms;
- sem congelar controle por longos períodos.

O golpe mais forte deve parecer forte principalmente pelo contraste entre preparação e impacto.

## 12. Câmera

- enquadramento 16:9;
- visão top-down/3/4;
- arena cabe quase inteira no protótipo;
- câmera acompanha Noah suavemente em salas futuras;
- zoom e shake raros;
- evitar câmera "edit de anime" a cada carta.

## 13. HUD

No combate:

- vida pequena no canto superior esquerdo;
- Cadência discreta perto dela;
- Pulso próximo de Noah;
- 4 cartas grandes o suficiente para leitura rápida na parte inferior;
- nome da carta + ícone + tecla;
- sem minimapa na arena inicial.

## 14. Métricas locais

Registrar em JSON:

- duração da sessão;
- cartas usadas por tipo;
- distribuição Livre/Bom/Perfeito;
- dano dado/recebido;
- tempo médio em cada Cadência;
- mortes;
- cartas que mais iniciam/finalizam sequências.

Sem envio de rede.

## 15. Critério de aprovação

Antes de adicionar conteúdo, o protótipo precisa cumprir:

1. mover Noah é agradável sem inimigos;
2. jogar quatro cartas seguidas produz decisões, não só spam;
3. timing melhora a experiência sem punir quem ignora o ritmo;
4. é possível reconhecer cada carta pela sensação;
5. chegar a S/RÉQUIEM muda claramente o clima;
6. um duelo contra Peregrino Oco é divertido por pelo menos 90 segundos em repetição.

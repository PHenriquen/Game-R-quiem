# RÉQUIEM — Direção de Áudio V1

## 1. Papel do som

Em Réquiem, áudio não é camada de acabamento.

Ele informa:

- estado do mundo;
- proximidade do Pulso;
- qualidade do timing;
- Cadência;
- presença de memória;
- diferença entre exploração e domínio.

O jogo deve continuar jogável sem áudio, mas perde parte intencional da emoção quando está mudo.

## 2. Ideia central

**O jogador não acompanha uma música pronta. Ele ajuda a música a voltar.**

Baixa Cadência:

- ambiente;
- poucas notas;
- bastante espaço.

Alta Cadência:

- pulso aparece;
- harmonia ganha corpo;
- sinos respondem;
- percussão entra.

RÉQUIEM:

- a composição finalmente parece completa por alguns segundos.

Quando o jogador perde fluxo, não cortar tudo de forma brusca. As camadas se afastam novamente.

## 3. BPM e compasso do protótipo

- BPM inicial: **100**;
- compasso: **4/4**;
- duração de beat: **0,60 s**;
- motivo recorrente: quatro ataques/notas/pulsos.

100 BPM é provisório. Deve ser testado entre aproximadamente 92–112 antes de virar regra final.

## 4. Motivo do Último Acorde

Motivo musical provisório de quatro notas:

**D → F → A → C♯**

Função:

- D/F/A cria uma base menor reconhecível;
- C♯ impede sensação de encerramento confortável;
- o quarto som precisa parecer que pede continuação.

O motivo pode aparecer:

- incompleto no Santuário;
- nos quatro sinos da Galeria;
- em ataques do Guardião;
- em um Eco da segunda presença;
- completo apenas em momento narrativo posterior.

A tonalidade final pode mudar. O importante é preservar a ideia de **quatro passos cujo último não resolve**.

## 5. Stems da Catedral

Todos os stems devem iniciar e loopar exatamente juntos.

### Stem 0 — Ambiente

Sempre presente.

- sala grande;
- água;
- pedra;
- ar;
- ressonância distante;
- nenhum ruído constante agressivo.

### Stem 1 — Memória

D/C.

- piano muito espaçado ou corda suave;
- motivo incompleto;
- notas com cauda longa.

### Stem 2 — Pulso

B.

- percussão baixa;
- textura seca a cada beat/half-beat conforme necessário;
- precisa sustentar combate sem virar música eletrônica genérica.

### Stem 3 — Sinos

A/S.

- bronze, harmônicos e pequenos elementos melódicos;
- não tocar sino grande a cada segundo;
- sensação de arquitetura respondendo.

### Stem 4 — RÉQUIEM

RÉQUIEM.

- camada que completa harmonia/ritmo;
- presença mais clara do motivo de quatro notas;
- pode incluir textura vocal sem palavras futuramente;
- deve soar como peça que sempre esteve faltando, não como outra música começando.

## 6. Transição por Cadência

Não mapear cada rank para volume máximo/zero de forma rígida.

Direção inicial:

- D: ambiente + memória muito fraca;
- C: memória clara;
- B: Pulso começa a entrar;
- A: Pulso + primeiros sinos;
- S: mix quase completo;
- RÉQUIEM: stem final + pequena reorganização de mix.

Transições usam fades curtos e preservam posição de playback.

## 7. Técnica no Godot

Para stems que precisam começar exatamente juntos, investigar/usar `AudioStreamSynchronized`.

Para gameplay dependente do beat, não calcular posição apenas acumulando `delta` quando música real estiver tocando.

Fonte de tempo recomendada para a primeira implementação com faixa:

`AudioStreamPlayer.GetPlaybackPosition()`

+

`AudioServer.GetTimeSinceLastMix()`

−

`AudioServer.GetOutputLatency()`

A documentação oficial do Godot alerta sobre buffer/mix/latência e recomenda compensar o tempo ouvido quando sincronização com BPM é importante.

### Calibração

Adicionar futuramente:

- offset visual;
- offset de input;
- teste simples de calibração;
- opção de ampliar janela Boa;
- Pulso visual independente de cor.

## 8. Feedback por timing

### Livre

Som normal do ataque.

Nenhuma bronca sonora.

### Bom

- camada curta de transiente;
- fragmento responde;
- pequeno reforço harmônico.

### Perfeito

- contato mais limpo;
- transiente mais definido;
- micro-resposta musical afinada ao stem atual;
- sem narrador dizendo “Perfect”.

O feedback visual pode mostrar texto durante protótipo. Na arte final, preferir som, carta, fragmento e movimento.

## 9. SFX das quatro ações

### Corte Breve

- ataque fino;
- transiente seco;
- cauda quase inexistente;
- impacto separado do movimento.

### Agulha

- ataque estreito;
- tom espectral curto;
- Perfeito recebe segundo impacto/eco 120 ms depois.

### Passo Fantasma

- remover energia sonora no início, depois reaparecer;
- evitar “whoosh” genérico gigante;
- afterimage pode ter camada reversa bem baixa.

### Sino Partido

Assinatura do primeiro protótipo.

Estrutura:

1. preparação quase silenciosa;
2. pequeno ruído metálico;
3. contato grave e curto;
4. anel bronzeado com cauda;
5. água/ambiente responde.

O peso vem do contraste.

## 10. Som de inimigos

Inimigos precisam ser legíveis sem olhar para eles o tempo inteiro.

### Peregrino Oco

- tecido/água nos passos;
- telegraph com som curto e reconhecível;
- golpe não pode usar o mesmo timbre de Nox.

### Cantor Partido

- preparação ocupa dois Pulsos;
- projétil deve possuir tom que permita localizar ameaça;
- evitar voz humana clara cedo demais.

## 11. Silêncio como mecânica

O **Corredor sem Eco** remove camadas progressivamente.

Importante: não mutar o computador/jogo inteiro instantaneamente.

Pode preservar:

- passos abafados;
- água;
- respiração/tecido;
- quase nenhum feedback das cartas.

Quando uma voz humana aparece depois de horas de poucos diálogos, ela ganha peso naturalmente.

## 12. Segunda presença

A segunda pessoa possui um elemento musical próprio que é, na verdade, metade complementar do motivo de Nox.

Não tocar a versão completa cedo.

Pistas possíveis:

- mesma nota com timbre diferente;
- resposta em intervalo;
- motivo invertido;
- nota final sustentada que Nox nunca completa sozinho.

O jogador pode reconhecer musicalmente a conexão antes de entender narrativamente.

## 13. Protótipo sem compositor final

Antes de produzir trilha definitiva, usar áudio original/procedural muito simples para validar:

- BPM;
- stems;
- ganho de Cadência;
- transição;
- janelas de timing;
- sensação de RÉQUIEM.

Não escolher trilha final em biblioteca genérica e depois construir identidade ao redor dela.

## 14. Critério de aprovação

Uma sessão de teste precisa responder:

1. consigo sentir o Pulso sem olhar para HUD?;
2. consigo jogar ignorando o Pulso?;
3. Perfeito parece melhor sem parecer obrigatório?;
4. subir Cadência enriquece a música sem trocar de faixa?;
5. RÉQUIEM parece conclusão temporária da música?;
6. perder Cadência parece perda de presença, não punição irritante?;

Se as respostas forem sim, o sistema está cumprindo sua função.

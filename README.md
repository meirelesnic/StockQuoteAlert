Aplicação Console que realiza o monitoramento da cotação de qualquer ativo da B3 escolhido: se a cotação subir acima de um determinado nível ou descer acima de outro,
um email será enviado aconselhando a compra ou a venda, dependendo do resultado.

Primeiramente, é necessário fazer as alterações dos valores do arquivo de configuração, App.config.

  1) Para as configurações da API, é necessário entrar no site https://brapi.dev/dashboard e criar um novo token para inserir no campo "AkiKey".
  
  ![image](https://github.com/meirelesnic/StockQuoteAlert/assets/93095788/d25a0cc9-6fe9-4a9f-8769-e5e03aaad708)
  
  2) Em seguida, é necessário os valores das configurações de acesso ao servidor de SMTP:
     - Sender: quem enviará o email.
     - Passoword: senha do remetente.
     - Recipient: quem receberá o email.
     - Host: o nome do servidor SMTP. Por exemplo, se for enviar utilizando o hotmail, preencher com "smtp-mail.outlook.com".
     - Port: é indicado que use a porta 587 por ser a porta recomendada para conexões SMTP seguras.

   ![image](https://github.com/meirelesnic/StockQuoteAlert/assets/93095788/0e1878da-3f54-4887-83dd-d3f0cc71ce7e)

Para executar a aplicação através da linha de comando, é necessário compilá-la, copiar o caminho do seu diretório até a pasta de Debug e seguir o passo a passo:

    1) Apertar Win+R;
    2) Digitar sysdm.cpl e apertar "Ok";
    3) Abrirá a tela de propriedades do sistema. Clicar em "Avançado";
    4) Clicar em "Variáveis de Ambiente";
    5) Em "Variáveis do sistema", clicar em "Path" duas vezes;
    6) Clicar em "Novo";
    7) Colar o caminho que foi copiado e apertar "Ok" nessa página e na anterior.

Agora, já é possível executar a aplicação pela linha de comando, passando o Ticker e os valores de referência como no exemplo:

QuoteAlert.exe PETR4 22.67 22.59

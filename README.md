# Cenário multithread com uso de variável estática

## Primeiros passos

O código é bem simples. Sugiro que você leia de forma breve o código antes de ler a explicação abaixo.

## Explicação

Código ajuda a entender o comportamento de um cenário multithread onde duas tarefas utilizam a mesma referência estática.

No exemplo em questão, temos um método "ImprimeIdade" que imprime um texto composto de um prefixo, seguido de uma idade.
Ex: Minha idade é: 30 anos.

A idade impressa no texto é obtida a partir do valor de uma variável estática do programa.

O programa chama as duas tarefas em sequencia, alterando a variável estática "idade" e o prefixo, de acordo com o desejado.

```csharp
    IDADE = 30;
    var taskImprimeMinhaIdade = Task.Run(() => ImprimeIdade("Minha idade é"));

    IDADE = 90;
    var taskImprimeIdadeAvo = Task.Run(() => ImprimeIdade("A idade da minha vó é"));
```

O resultado esperado é:

```text
Minha idade é: 30 anos.
A idade da minha vó é: 90 anos.
```

Porém, dependendo da ordem que as tarefas são executadas pelo processador, além do resultado esperado, podemos ter o seguinte resultado:

```text
Minha idade é: 90 anos.
A idade da minha vó é: 90 anos.
```

O cenário inesperado ocorre quando a atribuição "IDADE = 90;" ocorre antes do comando "Console.WriteLine" do método "ImprimeIdade".

A fim de controlar a execução de múltiplas threads e ter controle sobre o momento em que as threads estarão em pontos desejados do nosso programa, adicionei alguns comandos de "sleep" ao longo do código:

```csharp
    IDADE = 30;
    var taskImprimeMinhaIdade = Task.Run(() => ImprimeIdade("Minha idade é"));

    Thread.Sleep(100);
    IDADE = 90;
    var taskImprimeIdadeAvo = Task.Run(() => ImprimeIdade("A idade da minha vó é"));
```

```csharp
    static async Task ImprimeIdade(string prefixo)
    {
        System.Threading.Thread.Sleep(300);
        Console.WriteLine($"{prefixo}: {IDADE} anos.");
    }
```

O primeiro "sleep", de 100ms, garantiria que o "Console.WriteLine" da primeira tarefa irá executar antes da atribuição "IDADE = 90;".
Mas isso não ocorre, porque o segundo "sleep", de 300ms, garante que a execução da atribuição "IDADE = 90;" seja executada antes do "Console.WriteLine".

Com os dois "sleep", o resultado é que o texto inesperado será impresso.
Se comentarmos o "sleep" de 300ms, voltaremos a ter o texto desejado impresso.

Espero ter ajudado a esclarecer como cenários de multi thread podem trazer comportamentos inesperados nas nossas aplicações.

## Executando a aplicação

A partir de um terminal, na raiz do projeto, é possível executar o seguinte comando:

```
dotnet run .\dotnet-threads-with-static-variable.csproj
```

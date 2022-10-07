using System;

namespace puzzelNumeros
{
	// classe que guarda informações sobre a casa 
	class Numero{
		public int valor;
		public int linha;
		public int coluna;

		public Numero ()
		{
		
		}
		public Numero (int v,int l, int c){
			valor = v;
			linha=l;
			coluna =c; 
		}

	}

	class Puzzel
    {
		
		private int[,] tabuleiro; 
		private int maxLinha, maxColuna;
		private Numero casaZero= new Numero();


		public Puzzel(int l, int c)
		{
			maxLinha = l;
			maxColuna = c;  
			int contador = 0;
			tabuleiro = new int[l,c];
			// preenche o tabuleiro com a configuração 0,1,2,3,4,5,6,7,8
			for (int i = 0; i < maxLinha; i++) {
				for (int j = 0; j<maxColuna; j++) {
					tabuleiro [i, j] = contador;
					contador++;
				}
			}


		}
		// Esse método simula um número de jogadas para retirar o tabuleiro da configuração inicial
		public void embaralhar(int vezes)
        {
			Random sortCasa = new Random ();
			int numeroOpcoes=0;
			Numero[] vetOpcoes = new Numero[4];
			Numero casaEscolhida = new Numero();

			for (int cont = 0; cont < vezes; cont++)
            {
				this.opcoesJogada (ref numeroOpcoes, vetOpcoes);
				int opcaoJogador = sortCasa.Next (numeroOpcoes);
				// Dá um tempo só para evitar o sorteio de números repetidos
				System.Threading.Thread.Sleep(100);

				casaEscolhida = vetOpcoes[opcaoJogador];
				this.jogada(casaEscolhida);

				numeroOpcoes = 0;

			}
		
		}

		/* A lógica desse método é o seguinte: ele só dá vitória se ma matriz estiver ordenada. Para estar ordenada o número atual sempre deverá 
		   ser menor que o próximo. Se essa regra for quebra significa que o puzzel ainda não está correto
		   a única exeção é o caso do 0 que representa a casa vazia estar na ultima posição, mas isso é tratado no if interno aos for */
		public bool vitoria()
        {
			int menor = -1;
			int linha, coluna;
			for (linha = 0; linha < maxLinha; linha++) {
				for (coluna = 0; coluna<maxColuna; coluna++) {
					if (menor > tabuleiro [linha, coluna]) {
						// Esse if verifica se o 0 não é o ultimo número pois a sequencia 1,2,3,4,5,6,7,8,0 também é vitória 
						if((linha != maxLinha-1) || (coluna != maxColuna-1)){
							//Console.WriteLine ("Derrota");
							return false;
						}
					}
					// recebe o número que acabou de ser verificado
					menor = tabuleiro [linha, coluna];
				}
			}
			//Console.WriteLine ("Vitória");
			return true; 
		}
		// Esse método sempre limpa a tela e imprime o tabuleiro inteiro novamente 

		public void imprimir()
        {
			Console.Clear ();
			for (int i = 0; i < maxLinha; i++) {
				for (int j = 0; j<maxColuna; j++) {
					Console.Write (" | " + tabuleiro [i, j]);
				}

				Console.WriteLine (" | ");
			}
		}

		public void opcoesJogada(ref int vazio, Numero[] vetOpcoes)
        {
			

			// verifica se possui casa acima do 0
			if (casaZero.linha - 1 >= 0) {
				vetOpcoes [vazio] = new Numero (tabuleiro [casaZero.linha - 1, casaZero.coluna], casaZero.linha - 1, casaZero.coluna);
				vazio++;
			}
			// verifica se possui casa abaixo do 0 
			if (casaZero.linha + 1 < 3) {
				vetOpcoes [vazio] = new Numero (tabuleiro [casaZero.linha + 1, casaZero.coluna], casaZero.linha + 1, casaZero.coluna);
				vazio++;
			}
			// verifica se possui casa a direita do 0
			if (casaZero.coluna - 1 >=0) {
				vetOpcoes [vazio] = new Numero (tabuleiro [casaZero.linha, casaZero.coluna-1], casaZero.linha, casaZero.coluna-1);
				vazio++;
			}
			// verifica se possui casa a esquerda do 0
			if (casaZero.coluna + 1 < 3) {
				vetOpcoes [vazio] = new Numero (tabuleiro [casaZero.linha, casaZero.coluna+1], casaZero.linha, casaZero.coluna+1);
				vazio++;
			}

		}
		public void jogada(Numero casaEscolhida)
        {
			// Efetua a troca dos valores no tabuleiro  
			tabuleiro [casaZero.linha, casaZero.coluna] = tabuleiro [casaEscolhida.linha, casaEscolhida.coluna];
			tabuleiro [casaEscolhida.linha, casaEscolhida.coluna] = 0;

			// atalizando a posicao do 0;
			casaZero.linha = casaEscolhida.linha;
			casaZero.coluna = casaEscolhida.coluna;
		

		}

	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			// Cria um objeto do Jogo
			Puzzel Numeros = new Puzzel (4, 4);
			// Armazena as opcoes de jogada que o jogorador terá em função da posicao da casa 0
			Numero[] vetOpcoes = new Numero[4];
			// Armazena a casa da opção escolhida dentre as possiveis do vetOpcoes 
			Numero casaEscolhida = new Numero (); 
			// Controla quantas opcoes de jogada o jogador terá 
			int numeroOpcoes = 0;
			// A opcoao que o jogador escolheu
			int opcaoJogador; 
			// Chama o método que irá simular algumas jogadas para embaralhar a matriz
			Numeros.embaralhar (2);
			// Imprime a matriz na tela 
			Numeros.imprimir ();

			// Loop que controla o jogo

			while (Numeros.vitoria () == false) {
				// zera a as opções a cada jogada, pois ao movimentar o 0 as opções mudam 
				numeroOpcoes=0;
				// método que preenche o vetor com as possíveis jogadas 
				Numeros.opcoesJogada (ref numeroOpcoes, vetOpcoes);

				Console.WriteLine();
				Console.WriteLine("________ Escolha o número da opção que deseja movimentar ________");
				Console.WriteLine();
				for(int i=0; i< numeroOpcoes;i++){
					Console.WriteLine("Opção "+i+" - Nover o número "+ vetOpcoes[i].valor +" para casa 0");
				}
				Console.WriteLine();
				Console.Write("Digite o número da opção: ");
				opcaoJogador = int.Parse(Console.ReadLine());
				// verifica a valiadade da opção digitada 
				if(opcaoJogador >=0 && opcaoJogador<= numeroOpcoes){
					casaEscolhida = vetOpcoes[opcaoJogador];
					Numeros.jogada(casaEscolhida);
				}
				// imprime o tabuleiro
				Numeros.imprimir();

			} 

			Console.WriteLine ("Vitória");
            Console.ReadKey();
		
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AED
{
    public class No
    {
        public int valor;
        public No esquerda;
        public No direita;

        public No(int item)
        {
            valor = item;
            esquerda = direita = null;
        }
    }

    class ArvoreBinaria
    {
        No raiz;

        public ArvoreBinaria()
        {
            raiz = null;
        }

        public void Inserir(int valor)
        {
            raiz = InserirNo(raiz, valor);
        }

        private No InserirNo(No no, int valor)
        {
            if (no == null)
            {
                no = new No(valor);
                return no;
            }

            if (valor < no.valor)
            {
                no.esquerda = InserirNo(no.esquerda, valor);
            }
            else if (valor > no.valor)
            {
                no.direita = InserirNo(no.direita, valor);
            }

            return no;
        }

        public void Remover(int valor)
        {
            raiz = RemoverNo(raiz, valor);
        }

        public No RemoverNo(No no, int valor)
        {
            if (no == null)
            {
                return no;
            }

            if (valor < no.valor)
            {
                no.esquerda = RemoverNo(no.esquerda, valor);
            }
            else if (valor > no.valor)
            {
                no.direita = RemoverNo(no.direita, valor);
            }
            else
            {
                if (no.esquerda == null)
                {
                    return no.direita;
                }
                else if (no.direita == null)
                {
                    return no.esquerda;
                }

                no.valor = MenorValor(no.direita);

                no.direita = RemoverNo(no.direita, no.valor);
            }

            return no;
        }

        public int MenorValor(No no)
        {
            int menorValor = no.valor;
            while (no.esquerda != null)
            {
                menorValor = no.esquerda.valor;
                no = no.esquerda;
            }
            return menorValor;
        }

        public bool Buscar(int valor)
        {
            return BuscarNo(raiz, valor);
        }

        public bool BuscarNo(No no, int valor)
        {
            if (no == null)
            {
                return false;
            }

            if (valor == no.valor)
            {
                return true;
            }
            else if (valor < no.valor)
            {
                return BuscarNo(no.esquerda, valor);
            }
            else
            {
                return BuscarNo(no.direita, valor);
            }
        }

        public void Ordenada()
        {
            Ordenada(raiz);
        }

        private void Ordenada(No no)
        {
            if (no != null)
            {
                Ordenada(no.esquerda);
                Console.Write(no.valor + " ");
                Ordenada(no.direita);
            }
        } //exclusão de funcionarios 
    }
}

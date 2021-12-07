using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteCandidatoTriangulo
{
    public class Triangulo
    {
        /// <summary>
        ///    6
        ///   3 5
        ///  9 7 1
        /// 4 6 8 4
        /// um elemento somente pode ser somado com um dos dois elementos da próxima linha. como o elemento 5 na linha 2 pode ser somado com 7 e 1, mas não com o 9.
        /// neste triangulo o total máximo é 6 + 5 + 7 + 8 = 26
        /// 
        /// seu código deverá receber uma matriz (multidimensional) como entrada. O triângulo acima seria: [[6],[3,5],[9,7,1],[4,6,8,4]]
        /// </summary>
        /// <param name="dadosTriangulo"></param>
        /// <returns>Retorna o resultado do calculo conforme regra acima</returns>
        public int ResultadoTriangulo(string dadosTriangulo)
        {
            if (dadosTriangulo == string.Empty)
                return 0;


            List<List<int>> matrizes = new List<List<int>>();
            int soma = 0;
            int index = 0;

            var dataPrimary = dadosTriangulo.Split(']');

            foreach (var item in dataPrimary)
            {
                if (item == string.Empty)
                    break;

                string position = item.Remove(0, 2);
                List<int> positionList = position.Split(',').Select(Int32.Parse).ToList<int>();
                matrizes.Add(positionList);
            }

           
            for (int i = 0; i < matrizes.Count; i++)
            {
                if (i == 0)
                    soma += matrizes[i][i];
                else
                {
                    if (matrizes[i][index] > matrizes[i][index + 1])
                    {
                        soma += matrizes[i][index];
                    }
                    else
                    {
                        soma += matrizes[i][index + 1];
                        index += 1;
                    }
                }
            }

            return soma;
        }

    }
}

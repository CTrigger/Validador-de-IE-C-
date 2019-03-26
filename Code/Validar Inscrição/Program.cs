using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validar_Inscrição
{

    class IE
    {
        //Site da documentação
        //http://www.sintegra.gov.br/insc_est.html

        //gerador de testes
        //https://www.4devs.com.br/gerador_de_inscricao_estadual
        //observação o gerador do CE está errado no 4devs
        //observação o gerador do AL está errado no 4devs não trata o %11
        // 

        private string Inscricao;
        private string Estado;

        public IE(string Inscricao, string Estado)
        {
            this.Inscricao = Inscricao.Replace(".", "").Replace("/", "").Replace("-", "").Replace(" ", "").Trim();
            this.Estado = Estado.ToUpper().Trim();
        }
        public bool testa()
        {
            bool results;
            switch (Estado)
            {
                case "AC": results = AC_IE(); break;
                case "AL": results = AL_IE(); break;
                case "AP": results = AP_IE(); break;
                case "AM": results = AM_IE(); break;
                case "BA": results = BA_IE(); break;
                case "CE": results = CE_IE(); break;
                case "DF": results = DF_IE(); break;
                case "ES": results = ES_IE(); break;
                case "GO": results = GO_IE(); break;
                case "MA": results = MA_IE(); break;
                case "MT": results = MT_IE(); break;
                case "MS": results = MS_IE(); break;
                case "MG": results = MG_IE(); break;
                case "PA": results = PA_IE(); break;
                case "PB": results = PB_IE(); break;
                case "PR": results = PR_IE(); break;
                case "PE": results = PE_IE(); break;
                case "PI": results = PI_IE(); break;
                case "RJ": results = RJ_IE(); break;
                case "RN": results = RN_IE(); break;
                case "RS": results = RS_IE(); break;
                case "RO": results = RO_IE(); break;
                case "RR": results = RR_IE(); break;
                case "SC": results = SC_IE(); break;
                case "SP": results = SP_IE(); break;
                case "SE": results = SE_IE(); break;
                case "TO": results = TO_IE(); break;

                default:
                    results = false;
                    break;
            }
            return results;
        }
        #region Testado
        private bool AC_IE()
        {
            if (Inscricao.Length != 13 && Inscricao.Substring(0, 2) != "01")
            {
                return false;
            }
            else
            {
                int[] peso = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador1 = 0;
                int validador2 = 0;

                for (int i = 1; i < peso.Length; i++)
                {
                    validador1 += peso[i] * Convert.ToInt32(Inscricao.Substring(i - 1, 1));
                    validador2 += peso[i - 1] * Convert.ToInt32(Inscricao.Substring(i - 1, 1));

                }
                validador1 = validador1 % 11 < 2 ? 0 : 11 - validador1 % 11;
                validador2 += validador1 * peso[peso.Length - 1];
                validador2 = validador2 % 11 < 2 ? 0 : 11 - validador2 % 11;

                if (Inscricao.Substring(11, 1) == validador1.ToString() && Inscricao.Substring(12, 1) == validador2.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }
        private bool AL_IE()
        {
            int tipoEmpresa = Convert.ToInt32(Inscricao.Substring(2, 1));
            if (
                    Inscricao.Length != 9 &&
                    (
                        tipoEmpresa != 0 ||
                        tipoEmpresa != 3 ||
                        tipoEmpresa != 5 ||
                        tipoEmpresa != 7 ||
                        tipoEmpresa != 8
                    )
                )
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < 8; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = (validador * 10) - ((validador * 10) / 11) * 11;
                if (validador > 9)
                {
                    validador = 0;
                }

                if (Inscricao.Substring(8, 1) == validador.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }


        }
        private bool AP_IE()
        {
            if (Inscricao.Substring(0, 2) != "03" && Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int p = 0;
                int d = 0;
                int grupo = Convert.ToInt32(Inscricao.Substring(2, 6));

                //grupo1
                if (0 < grupo && grupo <= 17000)
                {
                    p = 5;
                    d = 0;
                }
                //grupo2
                if (17000 < grupo && grupo <= 19022)
                {
                    p = 9;
                    d = 1;
                }
                //grupo3
                if (19022 < grupo)
                {
                    p = 0;
                    d = 0;
                }


                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int verificador = p;
                for (int i = 0; i < 8; i++)
                {
                    verificador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                verificador = 11 - (verificador % 11);
                switch (verificador)
                {
                    case 10:
                        verificador = 0;
                        break;
                    case 11:
                        verificador = d;
                        break;
                    default:
                        break;
                }
                if (Inscricao.Substring(8, 1) == verificador.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        private bool AM_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int verificador = 0;
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                for (int i = 0; i < 8; i++)
                {
                    verificador += Convert.ToInt32(Inscricao.Substring(i, 1)) * peso[i];
                }
                if (verificador < 11)
                {
                    verificador = 11 - verificador;
                }
                else
                {
                    verificador %= 11;
                    if (verificador <= 1)
                    {
                        verificador = 0;
                    }
                    else
                    {
                        verificador = 11 - verificador;
                    }
                }
                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == verificador)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private bool BA_IE()
        {
            /*Incrição estadual da bahia trabalha com padrão de 8 ou 9*/
            if (7 < Inscricao.Length && Inscricao.Length < 10)
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int digitoValidador1 = 0;
                int digitoValidador2 = 0;

                int[] Mod = { 10, 11 };
                int iMod = 0;
                if (
                    Convert.ToInt32(Inscricao.Substring(0, 1)) < 6 ||
                    Convert.ToInt32(Inscricao.Substring(0, 1)) == 8
                )
                {
                    iMod = 0;
                }
                else
                {
                    iMod = 1;
                }

                if (Inscricao.Length == 8) // 8 digitos
                {
                    #region digito 2 do validador
                    for (int i = 2; i < peso.Length; i++)
                    {
                        digitoValidador2 += peso[i] * Convert.ToInt32(Inscricao.Substring(i - 2, 1));
                    }

                    if (iMod == 0)
                    {
                        digitoValidador2 = digitoValidador2 % Mod[iMod] == 0 ? 0 : Mod[iMod] - (digitoValidador2 % Mod[iMod]);
                    }
                    else
                    {
                        digitoValidador2 = digitoValidador2 % Mod[iMod] < 2 ? 0 : Mod[iMod] - (digitoValidador2 % Mod[iMod]);
                    }
                    #endregion
                    #region digito 1 do validador
                    for (int i = 1; i < peso.Length - 1; i++)
                    {
                        digitoValidador1 += peso[i] * Convert.ToInt32(Inscricao.Substring(i - 1, 1));
                    }
                    digitoValidador1 += peso[peso.Length - 1] * digitoValidador2;
                    digitoValidador1 = digitoValidador1 % Mod[iMod] == 0 ? 0 : Mod[iMod] - digitoValidador1 % Mod[iMod];
                    //Mod[iMod] - digitoValidador1 % Mod[iMod];
                    #endregion
                }
                else //9 digitos
                {
                    #region digito 2 do validador
                    for (int i = 1; i < peso.Length; i++)
                    {
                        digitoValidador2 += peso[i] * Convert.ToInt32(Inscricao.Substring(i - 1, 1));
                    }
                    if (iMod == 0)
                    {
                        digitoValidador2 = digitoValidador2 % Mod[iMod] == 0 ? 0 : Mod[iMod] - (digitoValidador2 % Mod[iMod]);
                    }
                    else
                    {
                        digitoValidador2 = digitoValidador2 % Mod[iMod] < 2 ? 0 : Mod[iMod] - (digitoValidador2 % Mod[iMod]);
                    }
                    #endregion

                    #region digito 1 do validador
                    for (int i = 0; i < peso.Length - 1; i++)
                    {
                        digitoValidador1 += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                    }
                    digitoValidador1 += digitoValidador2 * peso[peso.Length - 1];
                    digitoValidador1 = digitoValidador1 % Mod[iMod] == 0 ? 0 : Mod[iMod] - digitoValidador1 % Mod[iMod];
                    #endregion
                }

                if (
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 2, 1)) == digitoValidador1 ||
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == digitoValidador2
                )
                {
                    return true;
                }
                else
                {
                    return false;

                }

            }
            else
            {
                return false;
            }

        }
        private bool CE_IE()
        {
            //validadores do ceará 8 + 1 validador
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                //int diferenca = validador % 11;
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }

            }
            return false;
        }
        private bool DF_IE()
        {
            //modelo  2 + 6 + 3 + 2(verificadores)
            //sendo os 2 primeiros sempre 07
            if (Inscricao.Substring(0, 2) != "07" || Inscricao.Length != (2 + 6 + 3 + 2))
            {
                return false;
            }
            else
            {
                int[] pesoA = { 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int digitoVerificador1 = 0;

                int[] pesoB = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int digitoVeriticador2 = 0;

                for (int i = 0; i < pesoA.Length; i++)
                {
                    digitoVerificador1 += pesoA[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                digitoVerificador1 = 11 - digitoVerificador1 % 11;
                digitoVerificador1 = digitoVerificador1 > 9 ? 0 : digitoVerificador1;

                for (int i = 0; i < pesoB.Length - 1; i++)
                {
                    digitoVeriticador2 += pesoB[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                digitoVeriticador2 += pesoB[pesoB.Length - 1] * digitoVerificador1;
                digitoVeriticador2 = 11 - digitoVeriticador2 % 11;
                digitoVeriticador2 = digitoVeriticador2 > 9 ? 0 : digitoVeriticador2;


                if (
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 2, 1)) == digitoVerificador1 ||
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == digitoVeriticador2
                    )
                {
                    return true;
                }

            }


            return false;
        }
        private bool ES_IE()
        {
            //formato  8+1(validador)
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;
                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
            }
            return false;

        }
        private bool GO_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                // regra particular da instituição
                if (
                    "11094402" == Inscricao.Substring(0, Inscricao.Length - 2) &&
                    (
                        Inscricao.Substring(Inscricao.Length - 1, 1) == "0" ||
                        Inscricao.Substring(Inscricao.Length - 1, 1) == "1"
                    )
                )
                {
                    return true;
                }

                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador %= 11;
                switch (validador)
                {
                    case 0:
                        validador = 0;
                        break;
                    case 1:
                        if (10103105 <= Convert.ToInt32(Inscricao) && Convert.ToInt32(Inscricao) <= 10119997)
                        {
                            validador = 1;
                        }
                        else
                        {
                            validador = 0;
                        }
                        break;
                    default:
                        validador = 11 - validador;
                        break;
                }

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
                /*
                 Observação: curiosamente o Exemplo usado ono site oficial, é validavel no estado de ES também 
                 */
            }

            return false;
        }
        private bool MA_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }

            }

            return false;
        }
        private bool MT_IE()
        {
            if (Inscricao.Length != 11)
            {
                return false;
            }
            else
            {
                int[] peso = { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
            }

            return false;
        }
        private bool MS_IE()
        {
            // 8 + 1
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int verificador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    verificador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                if (verificador % 11 == 0 || 11 - verificador % 11 > 9)
                {
                    verificador = 0;
                }
                else
                {
                    verificador = 11 - verificador % 11;
                }

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == verificador)
                {
                    return true;
                }

            }

            return false;
        }
        private bool MG_IE()
        {
            if (Inscricao.Length != 13)
            {
                return false;
            }
            else
            {
                int[] peso1 = { 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2 };
                int verificador1 = 0;
                string algarismos = "";
                int[] peso2 = { 3, 2, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int verificador2 = 0;

                for (int i = 0; i < peso1.Length; i++)
                {
                    algarismos += (peso1[i] * Convert.ToInt32(Inscricao.Substring(i, 1))).ToString();
                }
                foreach (var algarismo in algarismos)
                {
                    verificador1 += Convert.ToInt32(algarismo - '0');
                }
                verificador1 = 10 - verificador1 % 10;

                for (int i = 0; i < peso2.Length - 1; i++)
                {
                    verificador2 += peso2[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                verificador2 += peso2[peso2.Length - 1] * verificador1;
                verificador2 = verificador2 % 11 < 2 ? 0 : 11 - verificador2 % 11;

                if (
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 2, 1)) == verificador1 &&
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == verificador2
                    )
                {
                    return true;
                }
            }
            return false;
        }
        private bool PA_IE()
        {
            if (Inscricao.Substring(0, 2) != "15" && Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;

                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;
                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
            }
            return false;
        }
        private bool PB_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
            }
            return false;
        }
        private bool PR_IE()
        {
            if (Inscricao.Length != 10)
            {
                return false;
            }
            else
            {
                int[] peso1 = { 3, 2, 7, 6, 5, 4, 3, 2 };
                int validador1 = 0;

                int[] peso2 = { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                int validador2 = 0;

                for (int i = 0; i < peso1.Length; i++)
                {
                    validador1 += peso1[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador1 = validador1 % 11 < 2 ? 0 : 11 - validador1 % 11;

                for (int i = 0; i < peso2.Length - 1; i++)
                {
                    validador2 += peso2[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador2 += peso2[peso2.Length - 1] * validador1;

                validador2 = validador2 % 11 < 2 ? 0 : 11 - validador2 % 11;

                if (
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 2, 1)) == validador1 &&
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador2
                    )
                {
                    return true;
                }


            }

            return false;
        }
        private bool PE_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador1 = 0;
                int validador2 = 0;

                for (int i = 1; i < peso.Length; i++)
                {
                    validador1 += peso[i] * Convert.ToInt32(Inscricao.Substring(i - 1, 1));
                }
                validador1 = validador1 % 11 < 2 ? 0 : 11 - validador1 % 11;

                for (int i = 0; i < peso.Length - 1; i++)
                {
                    validador2 += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador2 += peso[peso.Length - 1] * validador1;
                validador2 = validador2 % 11 < 2 ? 0 : 11 - validador2 % 11;

                if (
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 2, 1)) == validador1 &&
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador2
                )
                {
                    return true;
                }

            }
            return false;
        }
        private bool PI_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;

                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }

                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }

            }

            return false;
        }
        private bool RJ_IE()
        {
            if (Inscricao.Length != 8)
            {
                return false;
            }
            else
            {
                int[] peso = { 2, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
            }

            return false;
        }
        private bool RN_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }

            }

            return false;
        }
        private bool RS_IE()
        {
            if (Inscricao.Length != 10)
            {
                return false;
            }
            else
            {
                int[] peso = { 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;

                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }

                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }

            }

            return false;
        }
        private bool RO_IE()
        {
            if (Inscricao.Length == (3 + 5 + 1) || Inscricao.Length == (13 + 1))
            {
                int[] peso = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                if (Inscricao.Length == (3 + 5 + 1))
                {
                    Inscricao = "00000000" + Inscricao.Substring(Inscricao.Length - 6, 6);
                }

                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = 11 - validador % 11 > 9 ? (11 - validador % 11) - 10 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        private bool RR_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int validador = 0;
                int[] peso = { 1, 2, 3, 4, 5, 6, 7, 8 };
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador %= 9;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }
            }

            return false;
        }
        private bool SC_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;

                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }

                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }

            }
            return false;
        }
        private bool SP_IE()
        {
            if (Inscricao.Length != 12)
            {
                return false;
            }
            else
            {
                int[] peso1 = { 1, 3, 4, 5, 6, 7, 8, 10 };
                int validador1 = 0;

                for (int i = 0; i < peso1.Length; i++)
                {
                    validador1 += peso1[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador1 = validador1 % 11 > 9 ? 0 : validador1 % 11;

                int[] peso2 = { 3, 2, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador2 = 0;

                for (int i = 0; i < peso2.Length; i++)
                {
                    validador2 += peso2[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador2 = validador2 % 11 > 9 ? 0 : validador2 % 11;

                if (
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 4, 1)) == validador1 &&
                    Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador2
                )
                {
                    return true;
                }


            }
            return false;
        }
        private bool SE_IE()
        {
            if (Inscricao.Length != 9)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }

            }
            return false;
        }
        private bool TO_IE()
        {
            if (Inscricao.Length != 11)
            {
                return false;
            }
            else
            {
                int[] peso = { 9, 8, 0, 0, 7, 6, 5, 4, 3, 2 };
                int validador = 0;
                for (int i = 0; i < peso.Length; i++)
                {
                    validador += peso[i] * Convert.ToInt32(Inscricao.Substring(i, 1));
                }
                validador = validador % 11 < 2 ? 0 : 11 - validador % 11;

                if (Convert.ToInt32(Inscricao.Substring(Inscricao.Length - 1, 1)) == validador)
                {
                    return true;
                }


            }

            return false;
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region AC
            List<string> AC = new List<string>();
            AC.Add("01.648.764/291-70");
            AC.Add("01.602.851/050-60");
            AC.Add("01.464.436/391-61");
            AC.Add("01.330.587/424-36");
            AC.Add("01.279.809/768-37");
            AC.Add("01.774.193/036-04");
            AC.Add("01.975.393/445-15");
            AC.Add("01.025.245/932-32");
            AC.Add("01.793.366/964-43");
            AC.Add("01.112.702/333-52");
            #endregion
            #region AL
            List<string> AL = new List<string>();
            AL.Add("248309650");
            AL.Add("248143255");
            AL.Add("248200658");
            AL.Add("248958305");
            AL.Add("248323822");
            AL.Add("248228749");
            AL.Add("248416316");
            AL.Add("248806963");
            AL.Add("248136380");
            AL.Add("248741497");
            #endregion
            #region AP
            List<string> AP = new List<string>();
            AP.Add("039076954");
            AP.Add("035775343");
            AP.Add("031459668");
            AP.Add("039353990");
            AP.Add("035961040");
            AP.Add("030439043");
            AP.Add("034099107");
            AP.Add("033750998");
            #endregion
            #region AM
            List<string> AM = new List<string>();
            AM.Add("72.990.642-6");
            AM.Add("59.061.955-1");
            AM.Add("95.071.250-7");
            AM.Add("43.239.818-0");
            AM.Add("59.461.398-1");
            AM.Add("12.361.092-3");
            AM.Add("85.878.780-6");
            AM.Add("72.312.309-8");
            AM.Add("52.813.549-0");
            AM.Add("75.802.241-7");
            #endregion
            #region BA
            List<string> BA = new List<string>();
            BA.Add("972997-66");
            BA.Add("835508-65");
            BA.Add("685778-77");
            BA.Add("313943-34");
            BA.Add("012760-68");
            BA.Add("579883-46");
            BA.Add("862056-21");
            BA.Add("316169-13");
            BA.Add("256209-60");
            BA.Add("271663-41");

            #endregion
            #region CE
            List<string> CE = new List<string>();
            CE.Add("79275509-0");
            CE.Add("17796627-0");
            CE.Add("54085977-0");
            CE.Add("48239822-1");
            CE.Add("39171497-0");
            CE.Add("05172903-2");
            CE.Add("81256500-2");
            CE.Add("28256650-3");
            CE.Add("79675213-3");
            CE.Add("97983910-6");
            #endregion
            #region DF
            List<string> DF = new List<string>();
            DF.Add("07224308001-56");
            DF.Add("07076293001-37");
            DF.Add("07428684001-81");
            DF.Add("07121757001-07");
            DF.Add("07102626001-71");
            DF.Add("07986553001-18");
            DF.Add("07153322001-60");
            DF.Add("07005853001-13");
            DF.Add("07502781001-30");
            DF.Add("07063334001-55");
            #endregion
            #region ES
            List<string> ES = new List<string>();
            ES.Add("09596766-4");
            ES.Add("12060775-1");
            ES.Add("27983754-2");
            ES.Add("67851592-1");
            ES.Add("13433677-1");
            ES.Add("43175280-0");
            ES.Add("51531428-5");
            ES.Add("17369489-6");
            ES.Add("08187995-4");
            ES.Add("13793815-2");
            #endregion
            #region GO
            List<string> GO = new List<string>();
            GO.Add("10.571.688-0");
            GO.Add("11.521.861-0");
            GO.Add("10.505.123-3");
            GO.Add("15.443.083-8");
            GO.Add("15.493.726-6");
            GO.Add("11.925.297-0");
            GO.Add("15.777.562-3");
            GO.Add("10.469.521-8");
            GO.Add("10.266.360-2");
            GO.Add("15.209.934-4");
            #endregion
            #region MA
            List<string> MA = new List<string>();
            MA.Add("12150988-5");
            MA.Add("12280162-8");
            MA.Add("12708664-1");
            MA.Add("12647620-9");
            MA.Add("12744832-2");
            MA.Add("12578959-9");
            MA.Add("12937745-7");
            MA.Add("12814168-9");
            MA.Add("12744310-0");
            MA.Add("12737876-6");
            #endregion
            #region MT
            List<string> MT = new List<string>();
            MT.Add("1471536664-5");
            MT.Add("0415765408-0");
            MT.Add("8078474086-9");
            MT.Add("4353511340-8");
            MT.Add("7830039713-1");
            MT.Add("5152794582-4");
            MT.Add("7887013748-0");
            MT.Add("9113443475-0");
            MT.Add("2152017572-0");
            MT.Add("1715989459-4");
            #endregion
            #region MS
            List<string> MS = new List<string>();
            MS.Add("28141871-3");
            MS.Add("28365458-9");
            MS.Add("28353542-3");
            MS.Add("28323072-0");
            MS.Add("28568975-4");
            MS.Add("28764725-0");
            MS.Add("28496849-8");
            MS.Add("28228756-6");
            MS.Add("28614398-4");
            MS.Add("28904133-3");
            #endregion
            #region MG
            List<string> MG = new List<string>();
            MG.Add("3.095.205/0080");
            MG.Add("062.307.904/0081");
            MG.Add("712.716.127/4922");
            MG.Add("278.272.495/1729");
            MG.Add("704.832.248/0734");
            MG.Add("854.163.307/7852");
            MG.Add("933.516.784/6510");
            MG.Add("170.555.437/5850");
            MG.Add("967.115.267/9664");
            MG.Add("795.492.292/2541");
            MG.Add("231.123.221/3933");
            MG.Add("749.142.181/6150");

            #endregion
            #region PA
            List<string> PA = new List<string>();
            PA.Add("15-202288-0");
            PA.Add("15-763523-6");
            PA.Add("15-062504-9");
            PA.Add("15-158967-4");
            PA.Add("15-041770-5");
            PA.Add("15-878499-5");
            PA.Add("15-178037-4");
            PA.Add("15-341772-2");
            PA.Add("15-858727-8");
            PA.Add("15-139809-7");
            #endregion
            #region PB
            List<string> PB = new List<string>();
            PB.Add("92777193-4");
            PB.Add("42273486-1");
            PB.Add("33715039-7");
            PB.Add("78925909-5");
            PB.Add("71766583-6");
            PB.Add("14852037-5");
            PB.Add("24580377-7");
            PB.Add("94808886-9");
            PB.Add("54310473-7");
            PB.Add("87334368-9");
            #endregion
            #region PR
            List<string> PR = new List<string>();
            PR.Add("055.63961-24");
            PR.Add("355.16367-97");
            PR.Add("552.80071-06");
            PR.Add("785.38748-11");
            PR.Add("934.80385-05");
            PR.Add("710.77165-00");
            PR.Add("663.99497-27");
            PR.Add("487.25406-15");
            PR.Add("066.14957-90");
            PR.Add("646.96760-03");
            #endregion
            #region PE
            List<string> PE = new List<string>();
            PE.Add("4573501-80");
            PE.Add("4692965-77");
            PE.Add("6060591-05");
            PE.Add("9067028-01");
            PE.Add("3616054-71");
            PE.Add("3075696-08");
            PE.Add("5733726-83");
            PE.Add("7074854-36");
            PE.Add("0968490-53");
            PE.Add("5109061-92");
            #endregion
            #region PI
            List<string> PI = new List<string>();
            PI.Add("78530517-3");
            PI.Add("34258159-7");
            PI.Add("10553893-0");
            PI.Add("39108594-8");
            PI.Add("11501315-6");
            PI.Add("10113193-3");
            PI.Add("31323929-0");
            PI.Add("67689699-5");
            PI.Add("87799044-1");
            PI.Add("43544825-0");
            #endregion
            #region RJ
            List<string> RJ = new List<string>();
            RJ.Add("76.350.35-3");
            RJ.Add("08.193.91-6");
            RJ.Add("30.016.07-6");
            RJ.Add("41.521.70-8");
            RJ.Add("35.300.70-8");
            RJ.Add("18.074.42-7");
            RJ.Add("05.576.07-5");
            RJ.Add("30.269.68-3");
            RJ.Add("96.627.46-7");
            RJ.Add("16.055.98-0");
            #endregion
            #region RN
            List<string> RN = new List<string>();
            RN.Add("20.466.389-0");
            RN.Add("20.128.453-7");
            RN.Add("20.710.695-9");
            RN.Add("20.041.534-4");
            RN.Add("20.155.638-3");
            RN.Add("20.957.406-2");
            RN.Add("20.771.315-4");
            RN.Add("20.831.149-1");
            RN.Add("20.283.650-9");
            RN.Add("20.110.173-4");
            #endregion
            #region RS
            List<string> RS = new List<string>();
            RS.Add("601/8294331");
            RS.Add("236/5753773");
            RS.Add("252/4886039");
            RS.Add("542/4491584");
            RS.Add("525/5429437");
            RS.Add("723/1729631");
            RS.Add("178/5679101");
            RS.Add("408/2679838");
            RS.Add("237/9737848");
            RS.Add("970/4577481");
            #endregion
            #region RO
            List<string> RO = new List<string>();
            RO.Add("6906942566018-8");
            RO.Add("4371178266283-2");
            RO.Add("5424238305201-0");
            RO.Add("9074278830663-5");
            RO.Add("8621050586647-1");
            RO.Add("5058959537154-6");
            RO.Add("9005419581834-2");
            RO.Add("8713707039160-2");
            RO.Add("7173680579491-6");
            RO.Add("4794728822370-5");
            #endregion
            #region RR
            List<string> RR = new List<string>();
            RR.Add("24096434-0");
            RR.Add("24071664-0");
            RR.Add("24982810-8");
            RR.Add("24484400-8");
            RR.Add("24189663-3");
            RR.Add("24895042-4");
            RR.Add("24446588-2");
            RR.Add("24617314-7");
            RR.Add("24701044-6");
            RR.Add("24414131-0");
            #endregion
            #region SP
            List<string> SP = new List<string>();
            SP.Add("489.075.870.250");
            SP.Add("332.499.183.024");
            SP.Add("620.251.574.325");
            SP.Add("967.335.270.991");
            SP.Add("741.940.236.674");
            SP.Add("062.531.454.999");
            SP.Add("057.403.178.372");
            SP.Add("348.070.163.027");
            SP.Add("679.817.653.406");
            SP.Add("310.080.287.270");
            #endregion
            #region SC
            List<string> SC = new List<string>();
            SC.Add("779.509.986");
            SC.Add("393.057.810");
            SC.Add("214.030.369");
            SC.Add("342.175.211");
            SC.Add("636.048.985");
            SC.Add("288.265.980");
            SC.Add("242.631.460");
            SC.Add("249.964.449");
            SC.Add("502.914.785");
            SC.Add("425.212.084");
            #endregion
            #region SE
            List<string> SE = new List<string>();
            SE.Add("37105313-7");
            SE.Add("75619282-0");
            SE.Add("66751305-1");
            SE.Add("57958552-2");
            SE.Add("34448253-7");
            SE.Add("64542741-1");
            SE.Add("27836977-4");
            SE.Add("48503245-7");
            SE.Add("76935608-7");
            SE.Add("00906773-6");
            #endregion
            #region TO
            List<string> TO = new List<string>();
            TO.Add("1803815857-0");
            TO.Add("7703203413-3");
            TO.Add("0603064547-4");
            TO.Add("1103079950-0");
            TO.Add("0903293123-5");
            TO.Add("7203638928-5");
            TO.Add("7303601607-4");
            TO.Add("1903468122-0");
            TO.Add("7503614890-1");
            TO.Add("9903724380-5");
            #endregion
            foreach (var nIE in AC)
            {
                IE inscri = new IE(nIE, "AC");
                Console.WriteLine(inscri.testa());
            }

            //Console.WriteLine("030123459".Substring(2, 6));
            Console.ReadKey();

        }
    }
}

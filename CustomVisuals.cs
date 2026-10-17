using System;
using System.Drawing;
using System.Windows.Forms;

namespace BRBLog
{
    internal static class CustomVisuals
    {
        // Método que cria e retorna o botão padronizado do sistema
        public static Button CriarBotaoPadrao(string textoBotao)
        {
            Button botao = new Button
            {
                Text = textoBotao,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(190, 40),
                UseVisualStyleBackColor = false,
                Cursor = Cursors.Hand // Opcional: deixa o cursor em formato de mãozinha ao passar por cima
            };

            // Remove a bordinha de foco padrão do Windows
            botao.FlatAppearance.BorderSize = 0;

            return botao;
        }
    }
}
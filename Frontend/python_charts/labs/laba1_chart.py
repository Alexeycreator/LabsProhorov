"""import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

# ----- Константы -----
c_dB = 0.8                     # коэффициент Рэлея, дБ/(км*мкм^4)
conversion = 4.34              # перевод дБ/км в неп/км

# ----- Диапазоны параметров -----
lambda_um = np.linspace(0.4, 2.0, 100)    # длины волн от 0.4 до 2.0 мкм
L_km = np.linspace(0, 10, 50)             # длины световода от 0 до 10 км

# ----- Создание сетки для 3D -----
L_mesh, lambda_mesh = np.meshgrid(L_km, lambda_um)

# Расчёт коэффициента затухания (неп/км) для каждой точки сетки
alpha_nep = c_dB / (conversion * lambda_mesh**4)

# Расчёт отношения мощностей
P_ratio = np.exp(-alpha_nep * L_mesh)

# ----- Построение 3D-графика -----
fig = plt.figure(figsize=(12, 8))
ax = fig.add_subplot(111, projection='3d')

# Поверхность с цветовой картой 'viridis'
surf = ax.plot_surface(L_mesh, lambda_mesh, P_ratio,
                       cmap='viridis', edgecolor='none', alpha=0.9)

# Подписи осей
ax.set_xlabel('Длина световода L, км', fontsize=12, labelpad=10)
ax.set_ylabel('Длина волны λ, мкм', fontsize=12, labelpad=10)
ax.set_zlabel('P_T / P_0', fontsize=12, labelpad=10)
ax.set_title('Зависимость прошедшей мощности\nот длины волны и длины волокна',
             fontsize=14, pad=20)

# Добавляем цветовую шкалу
cbar = fig.colorbar(surf, ax=ax, shrink=0.5, aspect=15, pad=0.1)
cbar.set_label('Отношение мощностей', fontsize=10)

# Настройка угла обзора для лучшей наглядности
ax.view_init(elev=30, azim=225)

# Сохранение в файл (высокое разрешение)
plt.savefig('power_ratio_3D.png', dpi=300, bbox_inches='tight')
plt.show()"""
import numpy as np
import plotly.graph_objects as go
from plotly.subplots import make_subplots

# ----- Константы и данные (как в предыдущем примере) -----
c_dB = 0.8
conversion = 4.34

lambda_um = np.linspace(0.4, 2.0, 100)
L_km = np.linspace(0, 10, 50)

L_mesh, lambda_mesh = np.meshgrid(L_km, lambda_um)
alpha_nep = c_dB / (conversion * lambda_mesh**4)
P_ratio = np.exp(-alpha_nep * L_mesh)

# ----- Создание поверхности -----
fig = go.Figure(data=[go.Surface(
    x=L_mesh,          # длина световода
    y=lambda_mesh,     # длина волны
    z=P_ratio,         # отношение мощностей
    colorscale='Viridis',
    showscale=True,
    colorbar=dict(title='P_T / P_0')
)])

# ----- Настройка внешнего вида -----
fig.update_layout(
    title=dict(
        text='Зависимость прошедшей мощности<br>от длины волны и длины световода',
        x=0.5,
        font=dict(size=18)
    ),
    scene=dict(
        xaxis=dict(title='Длина световода L, км', gridcolor='lightgray'),
        yaxis=dict(title='Длина волны λ, мкм', gridcolor='lightgray'),
        zaxis=dict(title='P_T / P_0', range=[0, 1.05], gridcolor='lightgray'),
        camera=dict(
            eye=dict(x=1.8, y=1.8, z=1.2)   # начальный ракурс
        )
    ),
    width=900,
    height=700,
    margin=dict(l=0, r=0, b=0, t=50)
)

# ----- Сохранение в HTML (интерактивный) -----
fig.write_html('power_ratio_3D_interactive.html')

# ----- Сохранение в PNG (статичный, требует kaleido) -----
# fig.write_image('power_ratio_3D.png', width=900, height=700, scale=2)

# Показать в браузере (если запускаете скрипт локально)
fig.show()
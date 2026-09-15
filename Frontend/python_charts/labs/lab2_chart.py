import json
from pathlib import Path
import numpy as np
import plotly.graph_objects as go
def find_labs_json():
    current = Path(__file__).resolve().parent
    while current != current.parent:
        candidate = current / 'Backend' / 'WinApp' / 'WinApp' / 'Files' / 'Labs.json'
        if candidate.exists():
            return candidate
        current = current.parent
    raise FileNotFoundError("Labs.json не найден выше по дереву папок")

json_path = find_labs_json()

with open(json_path, 'r', encoding='utf-8') as f:
    data = json.load(f)

lab2 = data["Labs_2"]
wave_range = lab2["WaveRange"]# [1.0, 2.0] мкм
L_range    = lab2["WaveguideLengthRange"]# [100.0, 1000.0] км
P0         = lab2["InputPower"]


c_dB = 0.8
conversion = 4.34

lambda_um = np.linspace(wave_range[0], wave_range[1], 100)
L_km      = np.linspace(L_range[0],    L_range[1],    50)

L_mesh, lambda_mesh = np.meshgrid(L_km, lambda_um)
alpha_nep = c_dB / (conversion * lambda_mesh**4)
P_ratio   = np.exp(-alpha_nep * L_mesh)

fig = go.Figure(data=[go.Surface(
    x=L_mesh,
    y=lambda_mesh,
    z=P_ratio,
    colorscale='Viridis',
    showscale=True,
    colorbar=dict(title='P_T / P_0')
)])

fig.update_layout(
    title=dict(
        text='Зависимость прошедшей мощности<br>от длины волны и длины световода',
        x=0.5,
        font=dict(size=18)
    ),
    scene=dict(
        xaxis=dict(
            title='Длина световода L, км',
            range=[L_range[1],0],
            gridcolor='lightgray'
        ),
        yaxis=dict(
            title='Длина волны λ, мкм',
            range=[0, wave_range[1]],
            gridcolor='lightgray'
        ),
        zaxis=dict(
            title='P_T / P_0',
            range=[0, 1.05],
            gridcolor='lightgray'
        ),
        camera=dict(eye=dict(x=1.8, y=1.8, z=1.2)),
        aspectmode='cube'
    ),
    width=900,
    height=700,
    margin=dict(l=0, r=0, b=0, t=50)
)

fig.write_html('power_ratio_3D_interactive.html')
fig.show()
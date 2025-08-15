// src/App.tsx
import React, { useEffect, useState } from "react";
import { CallApi } from "./CallApi";

export default function App() {
  const API_BASE = "http://localhost:5234/api";

  const [contadorRecibido, setContadorRecibido] = useState<number>(0);
  const [mensajeRecibido, setMensajeRecibido] = useState<string>("");

  const [contadorEnviar, setContadorEnviar] = useState<number>(0);
  const [mensajeEnviar, setMensajeEnviar] = useState<string>("");

  // Actualiza datos cada segundo
  useEffect(() => {
    const fetchData = async () => {
      const contador = await CallApi(`${API_BASE}/contador`, "SEND");
      const mensaje = await CallApi(`${API_BASE}/mensaje`, "SEND");
      setContadorRecibido(contador);
      setMensajeRecibido(mensaje);
    };

    fetchData();
    const interval = setInterval(fetchData, 1000);
    return () => clearInterval(interval);
  }, []);

  const enviarDatos = async () => {
    await CallApi(`${API_BASE}/contador`, "POST", contadorEnviar);
    await CallApi(`${API_BASE}/mensaje`, "POST", mensajeEnviar);
  };

  return (
    <div style={{ display: "flex", gap: "2rem", padding: "2rem" }}>
      {/* Lado izquierdo: recibido de C# */}
      <div style={{ flex: 1, border: "1px solid #ccc", padding: "1rem" }}>
        <h2>Recibido (C#)</h2>
        <p><strong>Contador:</strong> {contadorRecibido}</p>
        <p><strong>Mensaje:</strong> {mensajeRecibido}</p>
      </div>

      {/* Lado derecho: enviar a C# */}
      <div style={{ flex: 1, border: "1px solid #ccc", padding: "1rem" }}>
        <h2>Enviar (editable)</h2>
        <label>
          Contador:
          <input
            type="number"
            value={contadorEnviar}
            onChange={(e) => setContadorEnviar(Number(e.target.value))}
          />
        </label>
        <br /><br />
        <label>
          Mensaje:
          <input
            type="text"
            value={mensajeEnviar}
            onChange={(e) => setMensajeEnviar(e.target.value)}
          />
        </label>
        <br /><br />
        <button onClick={enviarDatos}>Enviar a C#</button>
      </div>
    </div>
  );
}

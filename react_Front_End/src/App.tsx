import React from "react";
import { useApiData } from "./useApiData";

export default function App() {
  const API_BASE = "http://localhost:5234/api";
  const {
    contadorRecibido,
    mensajeRecibido,
    contadorEnviar,
    mensajeEnviar,
    setContadorEnviar,
    setMensajeEnviar,
    enviarDatos,
  } = useApiData(API_BASE);

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

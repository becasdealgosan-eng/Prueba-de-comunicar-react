import React, { FC, ReactNode, useState, useEffect } from "react";

// Caja que muestra la lista
type CajaProps = {
  items: string[];
};

const Caja: FC<CajaProps> = ({ items }) => (
  <div style={{ border: "1px solid #ccc", padding: "8px" }}>
    <ul>
      {items.map((item, i) => (
        <li key={i}>{item}</li>
      ))}
    </ul>
  </div>
);

const App: FC = () => {
  const [lista, setLista] = useState<string[]>([]);

  useEffect(() => {
    fetch("http://localhost:5234/api/lista")
      .then((res) => res.json())
      .then((data) => setLista(data))
      .catch((err) => console.error("Error al obtener lista:", err));
  }, []);

  return (
    <div style={{ padding: "16px", fontFamily: "sans-serif" }}>
      <h1>Lista desde API C#</h1>
      <Caja items={lista} />
    </div>
  );
};

export default App;

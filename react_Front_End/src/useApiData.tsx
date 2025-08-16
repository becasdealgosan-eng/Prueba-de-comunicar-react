import { useEffect, useState } from "react";
import { CallApi } from "./CallApi";

export function useApiData(apiBase: string) {
  const [contadorRecibido, setContadorRecibido] = useState<number>(0);
  const [mensajeRecibido, setMensajeRecibido] = useState<string>("");

  const [contadorEnviar, setContadorEnviar] = useState<number>(0);
  const [mensajeEnviar, setMensajeEnviar] = useState<string>("");

  // Actualiza datos cada segundo
  useEffect(() => {
    const fetchData = async () => {
      const contador = await CallApi(`${apiBase}/contador`, "SEND");
      const mensaje = await CallApi(`${apiBase}/mensaje`, "SEND");
      setContadorRecibido(contador);
      setMensajeRecibido(mensaje);
    };

    fetchData();
    const interval = setInterval(fetchData, 1000);
    return () => clearInterval(interval);
  }, [apiBase]);

  const enviarDatos = async () => {
    await CallApi(`${apiBase}/contador`, "POST", contadorEnviar);
    await CallApi(`${apiBase}/mensaje`, "POST", mensajeEnviar);
  };

  return {
    contadorRecibido,
    mensajeRecibido,
    contadorEnviar,
    mensajeEnviar,
    setContadorEnviar,
    setMensajeEnviar,
    enviarDatos,
  };
}

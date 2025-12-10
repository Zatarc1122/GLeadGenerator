import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { ResponseHandler } from "./components";
import "./index.css";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <ResponseHandler>
      <App />
    </ResponseHandler>
  </React.StrictMode>
);

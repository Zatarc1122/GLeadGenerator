import { QueryClient, QueryClientProvider } from "react-query";
import { ColorTabs, ResponseHandler } from "./components";

import "./App.css";

function App() {
  const queryClient = new QueryClient();
  return (
    <div className="App">
      <QueryClientProvider client={queryClient}>
        <ColorTabs />
      </QueryClientProvider>
    </div>
  );
}

export default App;

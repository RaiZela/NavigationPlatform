import { useState } from "react";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";
import { Card } from "primereact/card";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import "primeflex/primeflex.css";

export default function LoginPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = () => {
    // Add your login logic here
    console.log("Logging in with", username, password);
  };

  return (
    <div className="flex align-items-center justify-content-center h-screen px-3">
      <Card title="Login" className="w-full" style={{ maxWidth: 400 }}>
        <div className="flex flex-column gap-3">
          <div className="field">
            <label htmlFor="username" className="block mb-2">
              Username
            </label>
            <InputText
              id="username"
              className="w-full"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              placeholder="Enter your username"
            />
          </div>

          <div className="field">
            <label htmlFor="password" className="block mb-2">
              Password
            </label>
            <Password
              id="password"
              className="w-full"
              inputClassName="w-full"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              feedback={false}
              toggleMask
              placeholder="Enter your password"
            />
          </div>

          <Button
            label="Login"
            icon="pi pi-sign-in"
            onClick={handleLogin}
            className="w-full"
          />
        </div>
      </Card>
    </div>
  );
}

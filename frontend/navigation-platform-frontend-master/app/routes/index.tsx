import { redirect } from "react-router";

export async function clientLoader({ request }: { request: Request }) {
  //TEST, only redirect to journeys

  console.log("TESTINGGG THIS", request.url, request);

  const jrnyRedirect = true;

  if (jrnyRedirect) {
    return redirect("/journeys");
  } else {
    return redirect("/login");
  }
}

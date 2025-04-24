// routes.ts
import {
  type RouteConfig,
  index,
  layout,
  route,
} from "@react-router/dev/routes";

export default [
  index("./routes/index.tsx"),
  route("/login", "./routes/login-page.tsx"),

  //TODO: DUHET TE BESH ADD NJE ROUTE KU TE PERFSHIJ KTO
  layout("./layouts/sidebar.tsx", [
    route("/journeys", "./routes/journey-list.tsx"),
    route("/journey-details", "./routes/journey-details.tsx"),
    route("/badge-reward", "./routes/badge.tsx"),
  ]),
] satisfies RouteConfig;

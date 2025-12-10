import { useQuery } from "react-query";
import { getBusinessUsers as getBusinessUsersApi } from "../../api";
import { UsersTable } from "../../components";

export function BusinessUsers() {
  const query = useQuery("users", getBusinessUsersApi);

  return (
    <>
      <h1>Our business users</h1>
      {query.data && query.data.length > 0 ? (
        <>
          <p>This probably shouldn't be public, but... ¯\_(ツ)_/¯</p>
          <UsersTable users={query.data} />
        </>
      ) : (
        <p>There are no business users registered.</p>
      )}
    </>
  );
}

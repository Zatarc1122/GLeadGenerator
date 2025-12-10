import { Button, Stack, TextField } from "@mui/material";
import { useForm } from "react-hook-form";
import { createUser } from "../../api";
import { CreateUser } from "../../models";

export function ContactPage() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
    },
  });

  return (
    <>
      <h1>Get in touch with us!</h1>
      <form
        onSubmit={handleSubmit((data) => {
          const user = {
            firstName: data.firstName,
            lastName: data.lastName,
            email: data.email,
          } as CreateUser;
          createUser(user);
        })}
      >
        <Stack spacing={2} style={{ alignItems: "center" }}>
          <TextField
            error={errors.firstName !== undefined}
            id="outlined-required"
            label="First name"
            helperText={errors.firstName?.message}
            {...register("firstName", {
              required: "First name is required",
              maxLength: 100,
            })}
          />

          <TextField
            error={errors.lastName !== undefined}
            id="outlined-required"
            label="Last name"
            helperText={errors.lastName?.message}
            {...register("lastName", {
              required: "Last name is required",
              maxLength: 100,
            })}
          />

          <TextField
            error={errors.email !== undefined}
            id="outlined-required"
            label="Email"
            helperText={errors.email?.message}
            {...register("email", {
              required: "Email name is required",
              maxLength: 320,
              pattern: {
                value: /\S+@\S+\.\S+/,
                message: "Must be a valid email format",
              },
            })}
          />

          <Button type="submit" variant="contained">
            Contact us
          </Button>
        </Stack>
      </form>
    </>
  );
}

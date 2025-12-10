import { Snackbar } from "@mui/material";
import MuiAlert, { AlertProps } from "@mui/material/Alert";
import axios from "axios";
import { forwardRef, useEffect, useState } from "react";

export function ResponseHandler({ children }: any) {
  const [responseInterceptor, setResponseInterceptor] = useState<
    number | undefined
  >();
  const [severity, setSeverity] = useState<AlertProps["severity"]>("info");
  const [showMessage, setShowMessage] = useState(false);
  const [message, setMessage] = useState("");

  useEffect(() => {
    setResponseInterceptor(
      axios.interceptors.response.use(
        (response) => {
          setShowMessage(true);
          setMessage("Success.");
          setSeverity("success");
          return response;
        },
        (error) => {
          if (error.response?.data?.error) {
            setMessage(error.response.data.error);
          } else if (
            error.response?.data?.errors &&
            error.response?.data?.title
          ) {
            setMessage(error.response.data.title);
          } else {
            setMessage(
              "An unexpected network error occurred. Try again later."
            );
          }

          setShowMessage(true);
          setSeverity("error");

          return Promise.reject(error);
        }
      )
    );
  }, []);

  useEffect(() => {
    return () => {
      if (responseInterceptor) {
        axios.interceptors.response.eject(responseInterceptor);
      }
    };
  }, [responseInterceptor]);

  const handleClose = (
    _event?: React.SyntheticEvent | Event,
    reason?: string
  ) => {
    if (reason === "clickaway") {
      return;
    }

    setShowMessage(false);
  };

  const Alert = forwardRef<HTMLDivElement, AlertProps>(function Alert(
    props,
    ref
  ) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
  });

  return (
    <>
      {children}
      <Snackbar
        open={showMessage}
        autoHideDuration={5000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity={severity} sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>
    </>
  );
}

import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { Link } from "react-router-dom";

export default function Hero() {
  return (
    <Container maxWidth="sm" sx={{ pt: 22, pb: 22 }}>
      <Typography variant="h2" align="center" color="text.primary" gutterBottom>
        Palace Memory
      </Typography>
      <Typography variant="h6" align="center" color="text.secondary" paragraph>
        Something short and leading about the collection below—its contents, the
        creator, etc. Make it short and sweet, but not too short so folks
        don&apos;t simply skip over it entirely.
      </Typography>
      <Stack sx={{ pt: 4 }} direction="row" spacing={2} justifyContent="center">
        <Button
          variant="contained"
          color="secondary"
          component={Link}
          to="register"
        >
          Get Started
        </Button>
      </Stack>
    </Container>
  );
}

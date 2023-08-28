import { createTheme, ThemeProvider } from '@mui/material/styles';
import Hero from '../components/Hero';
import Features from '../components/Features';


const defaultTheme = createTheme();

export default function Home() {
  return (
    <ThemeProvider theme={defaultTheme}>
        <Hero />
        <Features />
    </ThemeProvider>
  )
}

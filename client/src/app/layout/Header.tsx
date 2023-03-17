import { AppBar, FormControlLabel, FormGroup, Switch, Toolbar, Typography } from "@mui/material";

interface Props {
    mode: string;
    darkMode: boolean;
    handleThemeChange: () => void; 
}

export default function Header({mode, darkMode, handleThemeChange}: Props) {

    return (
        <AppBar position="static" sx={{mb: 4}}>
            <Toolbar>
                <Typography variant="h6" sx={{mr: 4}}>
                    RE-STORE
                </Typography>
                <FormGroup>
                    <FormControlLabel 
                        control={
                            <Switch checked={darkMode} 
                            onChange={handleThemeChange}/>
                        } 
                        label={mode.charAt(0).toUpperCase() + mode.slice(1) + ' Mode'} />
                </FormGroup>
            </Toolbar>
        </AppBar>
    )
}
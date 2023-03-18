import { ShoppingCart } from "@mui/icons-material";
import { AppBar, Badge, Box, FormControlLabel, FormGroup, IconButton, List, ListItem, Switch, Toolbar, Typography } from "@mui/material";
import { NavLink } from "react-router-dom";

const midLinks = [
    {title: 'catalog', path: '/catalog'},
    {title: 'about', path: '/about'},
    {title: 'contact', path: '/contact'},
]

const rightLinks = [
    {title: 'login', path: '/login'},
    {title: 'register', path: '/register'},
]

interface Props {
    mode: string;
    darkMode: boolean;
    handleThemeChange: () => void; 
}

const navStyles = {
    color: 'inherit', 
    textDecoration: 'none',
    typography: 'h6',
    '&:hover': {
        color: 'grey.500'
    },
    '&.active': {
        color: 'text.secondary'
    }
}

export default function Header({mode, darkMode, handleThemeChange}: Props) {

    return (
        <AppBar position="static" sx={{mb: 4}}>
            <Toolbar sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center'
            }}>
                <Box display='flex' alignItems='center'>
                    <Typography 
                        variant="h6" 
                        component={NavLink}
                        to='/'
                        sx={navStyles}
                    >
                        RE-STORE
                    </Typography>

                    {/* Light Mode/Dark Mode Switch */}
                    <FormGroup sx={{ml: 2}}>
                        <FormControlLabel 
                            control={
                                <Switch checked={darkMode} 
                                onChange={handleThemeChange}/>
                            } 
                            label={mode.charAt(0).toUpperCase() + mode.slice(1) + ' Mode'} />
                    </FormGroup>
                </Box>
                
                {/* Middle Links */}
                <List sx={{display: 'flex'}}>
                    {midLinks.map(({title, path}) => (
                        <ListItem
                            component={NavLink}
                            to={path}
                            key={path}
                            sx={navStyles}
                        >
                            {title.toUpperCase()}
                        </ListItem>
                    ))}
                </List>

                <Box  display='flex' alignItems='center'>
                    {/* Basket */}
                    <IconButton size="large" edge='start' color='inherit' sx={{mr: 2}}>
                        <Badge badgeContent='4' color='secondary'>
                            <ShoppingCart />
                        </Badge>
                    </IconButton>
                    {/* Right Links */}
                    <List sx={{display: 'flex'}}>
                        {rightLinks.map(({title, path}) => (
                            <ListItem
                                component={NavLink}
                                to={path}
                                key={path}
                                sx={navStyles}
                            >
                                {title.toUpperCase()}
                            </ListItem>
                        ))}
                    </List>
                </Box>
                
            </Toolbar>
        </AppBar>
    )
}
import React from 'react';
import Popover from '@material-ui/core/Popover';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles(theme => ({
    popoverContent: {
        padding: theme.spacing(2)
    }
}));

function InfoButton(props) {
    const { children } = props;

    const classes = useStyles();

    const [anchorEl, setAnchorEl] = React.useState(null);

    function handleClick(event) {
        setAnchorEl(event.currentTarget);
    }

    function handleClose() {
        setAnchorEl(null);
    }

    const open = Boolean(anchorEl);
    const id = open ? 'info-popover' : undefined;

    return (
        <div>
            <Button aria-describedby={id} color="primary" variant="outlined" onClick={handleClick}>
                Info
            </Button>
            <Popover
                id={id}
                open={open}
                anchorEl={anchorEl}
                onClose={handleClose}
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'center'
                }}
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'center'
                }}
            >
                <div className={classes.popoverContent}>{children}</div>
            </Popover>
        </div>
    );
}

export default InfoButton;

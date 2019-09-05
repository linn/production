import React from 'react';
import Popover from '@material-ui/core/Popover';
import IconButton from '@material-ui/core/IconButton';
import NotesIcon from '@material-ui/icons/Notes';
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles(theme => ({
    popoverContent: {
        padding: theme.spacing(2)
    }
}));

function NotesPopover(props) {
    const { id, children } = props;

    const classes = useStyles();

    const [anchorEl, setAnchorEl] = React.useState(null);

    function handleClick(event) {
        setAnchorEl(event.currentTarget);
    }

    function handleClose() {
        setAnchorEl(null);
    }

    const open = Boolean(anchorEl);
    const popoverId = open ? { id } : undefined;

    return (
        <div>
            <IconButton
                aria-describedby={popoverId}
                color="primary"
                variant="outlined"
                onClick={handleClick}
            >
                <NotesIcon />
            </IconButton>
            <Popover
                id={popoverId}
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

export default NotesPopover;

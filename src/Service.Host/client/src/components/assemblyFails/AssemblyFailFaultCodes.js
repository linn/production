import React, { useEffect, useState, Fragment } from 'react';
import { Title, Loading, utilities, SnackbarMessage } from '@linn-it/linn-form-components-library';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import Tooltip from '@material-ui/core/Tooltip';
import AddIcon from '@material-ui/icons/Add';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';
import AssemblyFailFaultCode from './AssemblyFailFaultCode';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(3)
    },
    button: {
        maxWidth: theme.spacing(5),
        minWidth: theme.spacing(5),
        maxHeight: theme.spacing(3),
        minHeight: theme.spacing(3),
        padding: 0
    }
}));

// TODO do inline editing and set get the code from each item
// set the editing one in state
export default function AssemblyFailFaultCodes({ items, loading }) {
    const [selectedFaultCode, setSelectedFaultCode] = useState([]);
    const [faultCodes, setFaultCodes] = useState(false);
    const [prevItems, setPrevItems] = useState({});

    const classes = useStyles();

    useEffect(() => {
        if (items !== prevItems) {
            setFaultCodes(items);
            setPrevItems(items);
        }
    }, [items, prevItems]);

    const handleAddClick = () => {
        const toAdd = [{ faultCode: 'blomp' }];
        setFaultCodes(toAdd.concat(faultCodes));
    };

    const handleRemoveClick = () => {
        setFaultCodes(prevItems);
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Assembly Fail Fault Codes" />
                </Grid>
                {loading && (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                )}
                {faultCodes.length > 0 && (
                    <Fragment>
                        {/* <SnackbarMessage
                            visible={snackbarVisible}
                            onClose={() => setSnackbarVisible(false)}
                            message="Save Successful"
                        /> */}
                        <Table size="small" className={classes.marginTop}>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Fault Code</TableCell>
                                    <TableCell>Description</TableCell>
                                    <TableCell>Explanation</TableCell>
                                    <TableCell>Date Invalid</TableCell>
                                    <TableCell>
                                        <Button
                                            onClick={handleAddClick}
                                            color="primary"
                                            variant="outlined"
                                            size="small"
                                            classes={{
                                                root: classes.button
                                            }}
                                        >
                                            <AddIcon fontSize="small" />
                                        </Button>
                                    </TableCell>
                                    <TableCell />
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {utilities
                                    .sortEntityList(faultCodes, 'faultCode')
                                    .map(item =>
                                        item.faultCode ? (
                                            <AssemblyFailFaultCode
                                                key={item.faultCode}
                                                item={item}
                                            />
                                        ) : (
                                            <AssemblyFailFaultCode
                                                key="heythere"
                                                item={item}
                                                creating
                                                removeItem={handleRemoveClick}
                                            />
                                        )
                                    )}
                            </TableBody>
                        </Table>
                    </Fragment>
                )}
            </Grid>
        </Page>
    );
}

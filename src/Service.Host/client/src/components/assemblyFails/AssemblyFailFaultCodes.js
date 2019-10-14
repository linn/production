import React, { useEffect, useState, Fragment } from 'react';
import PropTypes from 'prop-types';
import {
    Title,
    Loading,
    utilities,
    SnackbarMessage,
    CreateButton,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';
import AssemblyFailFaultCodeRow from './AssemblyFailFaultCodeRow';

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

export default function AssemblyFailFaultCodes({
    items,
    loading,
    faultCodeLoading,
    snackbarVisible,
    setSnackbarVisible,
    updateAssemblyFailFaultCode,
    faultCodeError,
    faultCodesError
}) {
    const [faultCodes, setFaultCodes] = useState(false);
    const [prevItems, setPrevItems] = useState({});

    const classes = useStyles();

    useEffect(() => {
        if (items !== prevItems) {
            setFaultCodes(items);
            setPrevItems(items);
        }
    }, [items, prevItems]);

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Assembly Fail Fault Codes" />
                    <CreateButton createUrl="/production/quality/assembly-fail-fault-codes/create" />
                </Grid>
                {faultCodeError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={faultCodeError} />
                    </Grid>
                )}
                {faultCodesError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={faultCodesError} />
                    </Grid>
                )}
                {loading || faultCodeLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    faultCodes.length > 0 && (
                        <Fragment>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Table size="small" className={classes.marginTop}>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Fault Code</TableCell>
                                        <TableCell>Description</TableCell>
                                        <TableCell>Explanation</TableCell>
                                        <TableCell>Date Invalid</TableCell>
                                        <TableCell />
                                        <TableCell />
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {utilities.sortEntityList(faultCodes, 'faultCode').map(item => (
                                        <AssemblyFailFaultCodeRow
                                            key={item.faultCode}
                                            item={item}
                                            updateAssemblyFailFaultCode={
                                                updateAssemblyFailFaultCode
                                            }
                                        />
                                    ))}
                                </TableBody>
                            </Table>
                        </Fragment>
                    )
                )}
            </Grid>
        </Page>
    );
}

AssemblyFailFaultCodes.propTypes = {
    items: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    faultCodeLoading: PropTypes.bool,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    updateAssemblyFailFaultCode: PropTypes.func.isRequired,
    faultCodeError: PropTypes.string,
    faultCodesError: PropTypes.string
};

AssemblyFailFaultCodes.defaultProps = {
    items: [],
    loading: false,
    faultCodeLoading: false,
    snackbarVisible: false,
    faultCodeError: '',
    faultCodesError: ''
};

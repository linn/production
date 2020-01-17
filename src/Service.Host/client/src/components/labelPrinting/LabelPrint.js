import React, { useState, useCallback, useEffect, Fragment } from 'react';
import PropTypes from 'prop-types';
import {
    Dropdown,
    InputField,
    Loading,
    SearchInputField,
    Title,
    ErrorCard,
    SnackbarMessage,
    useSearch,
    utilities
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';
import { labelTypes } from '../../itemTypes';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(2)
    }
}));

function LabelPrint({
    loading,
    snackbarVisible,
    itemError,
    labelPrintTypes,
    labelPrinters,
    print,
    snackbarMessage
}) {
    const [labelType, setLabelType] = useState(0);
    const [printer, setPrinter] = useState(0);
    const [quantity, setQuantity] = useState(1);
    const [labelDetails, setLabelDetails] = useState([{}]);

    const classes = useStyles();

    useEffect(() => {}, []);

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'labelType') {
            setLabelType(newValue);
            //set label details/properties - rows etcccc
        }
        if (propertyName === 'printer') {
            setPrinter(newValue);
        }
    };

    const handleLabelDetailsChange = (line, newValue) => {};

    const handlePrintClick = () => {
        if (false) {
            print();
        }
        //send all details back - which type, printer and the deets
    };

    const handleClearClick = () => {
        //reset label details? Keep type and printer selected?
    };

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="General Purpose Label Printer" />
                </Grid>
                <SnackbarMessage
                    visible={snackbarVisible}
                    //onClose={() => setPrintAllLabelsForProductActionsMessageVisible(false)}
                    message={snackbarMessage}
                />
                <Grid item xs={12}>
                    {itemError && <ErrorCard errorMessage={itemError.errorMessage} />}
                </Grid>
                <Grid item xs={9} />
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Fragment>
                        <Grid item xs={3} className={classes.marginTop}>
                            <Dropdown
                                value={labelType || ''}
                                label="Label Type"
                                fullWidth
                                items={labelPrintTypes}
                                onChange={handleFieldChange}
                                propertyName="labelType"
                            />
                        </Grid>
                        <Grid item xs={3} className={classes.marginTop}>
                            <Dropdown
                                value={printer || ''}
                                label="Printer"
                                fullWidth
                                items={labelPrinters}
                                onChange={handleFieldChange}
                                propertyName="printer"
                            />
                        </Grid>
                        <Grid item xs={3} className={classes.marginTop}>
                            <InputField //Quantity
                                label="Quantity"
                                fullWidth
                                type="string"
                                onChange={handleFieldChange}
                                propertyName="quantity"
                                value={quantity}
                            />
                        </Grid>
                        {/* <Grid item xs={6} />
                        <InputField //inputs + logic
                            label="Line 1"
                            fullWidth
                            type="string"
                            onChange={handleFieldChange}
                            propertyName="articleNumber"
                            value={articleNumber}
                        /> */}

                        <Grid item xs={1} />
                        <Grid item xs={3} className={classes.marginTop}>
                            <Button onClick={handlePrintClick} variant="outlined" color="secondary">
                                Print
                            </Button>
                        </Grid>
                        <Grid item xs={3} className={classes.marginTop}>
                            <Button onClick={handleClearClick} variant="outlined" color="secondary">
                                Clear
                            </Button>
                        </Grid>
                        <Grid item xs={5} />
                    </Fragment>
                )}
            </Grid>
        </Page>
    );
}

LabelPrint.propTypes = {
    loading: PropTypes.bool,
    snackbarVisible: PropTypes.bool,
    itemError: PropTypes.shape({}),
    labelPrintTypes: PropTypes.arrayOf(PropTypes.shape({})),
    labelPrinters: PropTypes.arrayOf(PropTypes.shape({})),
    print: PropTypes.func.isRequired,
    snackbarMessage: PropTypes.string
};

LabelPrint.defaultProps = {
    loading: false,
    snackbarVisible: false,
    itemError: {},
    labelPrintTypes: [{}],
    labelPrinters: [{}],
    snackbarMessage: ''
};
export default LabelPrint;

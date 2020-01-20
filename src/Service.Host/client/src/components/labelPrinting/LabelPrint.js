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
import { labelTypes, labelPrint } from '../../itemTypes';

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
    const printLinesInitialState = [
        { id: 'supplierId', displayName: 'Supplier ID', value: '', width: 6 },
        { id: 'addressId', displayName: 'Address ID', value: '', width: 6 },
        { id: 'addressee', displayName: 'Addressee', value: '', width: 12 },
        { id: 'line1', displayName: 'Line 1', value: '', width: 12 },
        { id: 'line2', displayName: 'Line 2', value: '', width: 12 },
        { id: 'line3', displayName: 'Line 3', value: '', width: 12 },
        { id: 'line4', displayName: 'Line 4', value: '', width: 12 },
        { id: 'line5', displayName: 'Line 5', value: '', width: 12 },
        { id: 'line6', displayName: 'Line 6', value: '', width: 12 },
        { id: 'line7', displayName: 'Line 7', value: '', width: 12 },
        { id: 'postalCode', displayName: 'Postal Code', value: '', width: 3, onOwnLine: true },
        { id: 'country', displayName: 'Country', value: '', width: 9, onOwnLine: true },
        { id: 'fromPCNumber', displayName: 'From PC Number', value: '', width: 3 },
        { id: 'toPCNumber', displayName: 'To PC Number', value: '', width: 3 },
        { id: 'qty', displayName: 'Quantity', value: '', width: 12 },
        { id: 'initials', displayName: 'Initials', value: '', width: 12 },
        { id: 'date', displayName: 'Date', value: '', width: 3 },
        { id: 'partNumber', displayName: 'Part Number', value: '', width: 12 }
    ];

    const [labelType, setLabelType] = useState(0);
    const [printer, setPrinter] = useState(0);
    const [quantity, setQuantity] = useState(1);
    const [labelDetails, setLabelDetails] = useState(printLinesInitialState);

    const classes = useStyles();

    useEffect(() => {
        console.info(labelPrintTypes);
        console.info(labelPrinters);
    }, [labelPrintTypes, labelPrinters]);

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
        <Fragment>
            <Grid container alignItems="center" justify="center">
                <Grid xs={6} item>
                    <Page showRequestErrors>
                        <Grid item xs={12} container>
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
                            {loading ? (
                                <Grid item xs={12}>
                                    <Loading />
                                </Grid>
                            ) : (
                                <Fragment>
                                    <Grid item xs={12}>
                                        {/* className={classes.marginTop}> */}
                                        <Grid item xs={3}>
                                            <Dropdown
                                                value={labelType || ''}
                                                label="Label Type"
                                                fullWidth
                                                items={labelPrintTypes}
                                                onChange={handleFieldChange}
                                                propertyName="labelType"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <Dropdown
                                                value={printer || ''}
                                                label="Printer"
                                                fullWidth
                                                items={labelPrinters}
                                                onChange={handleFieldChange}
                                                propertyName="printer"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField //Quantity
                                                label="Quantity"
                                                fullWidth
                                                type="string"
                                                onChange={handleFieldChange}
                                                propertyName="quantity"
                                                value={quantity}
                                            />
                                        </Grid>
                                    </Grid>
                                    {labelDetails.map(line => (
                                        // if line is displayed for current label type
                                        <Fragment>
                                            <Grid xs={line.width}>
                                                <InputField //inputs + logic
                                                    label={line.displayName}
                                                    fullWidth
                                                    type="string"
                                                    onChange={handleFieldChange}
                                                    propertyName={line.id}
                                                    value={line.value}
                                                />
                                            </Grid>
                                            {line.onOwnLine && <Grid xs={12} />}
                                        </Fragment>
                                    ))}
                                    <Grid item xs={1} />
                                    <Grid item xs={3} className={classes.marginTop}>
                                        <Button
                                            onClick={handlePrintClick}
                                            variant="outlined"
                                            color="secondary"
                                        >
                                            Print
                                        </Button>
                                    </Grid>
                                    <Grid item xs={3} className={classes.marginTop}>
                                        <Button
                                            onClick={handleClearClick}
                                            variant="outlined"
                                            color="secondary"
                                        >
                                            Clear
                                        </Button>
                                    </Grid>
                                    <Grid item xs={5} />
                                </Fragment>
                            )}
                        </Grid>
                    </Page>
                </Grid>
            </Grid>
        </Fragment>
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

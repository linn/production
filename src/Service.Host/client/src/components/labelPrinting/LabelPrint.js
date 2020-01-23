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
    utilities,
    Typeahead
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
    },
    filtersWithSpacing: {
        marginBottom: theme.spacing(4),
        marginTop: theme.spacing(2)
    },
    boldText: {
        fontWeight: 'bold'
    },
    spacingRight: {
        marginRight: theme.spacing(2)
    },
    hide: {
        display: 'none'
    }
}));

function LabelPrint({
    loading,
    snackbarVisible,
    itemError,
    labelPrintTypes,
    labelPrinters,
    print,
    snackbarMessage,
    searchAddresses,
    addressSearchLoading,
    addressSearchResults,
    clearAddressSearch,
    supplierSearchLoading,
    supplierSearchResults,
    searchSuppliers,
    clearSupplierSearch
}) {
    const printLinesInitialState = [
        {
            id: 'supplierId',
            displayName: 'Supplier ID',
            value: '',
            width: 6,
            displayForLabelTypes: [4, 5],
            inputType: 'typeahead'
        },
        {
            id: 'addressId',
            displayName: 'Address ID',
            value: '',
            width: 6,
            displayForLabelTypes: [4, 5],
            inputType: 'typeahead'
        },
        {
            id: 'addressee',
            displayName: 'Addressee',
            value: '',
            width: 12,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'addressee2',
            displayName: 'Addressee 2',
            value: '',
            width: 12,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'line1',
            displayName: 'Line 1',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 1, 2, 4, 6, 7],
            inputType: 'string'
        },
        {
            id: 'line2',
            displayName: 'Line 2',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 2, 4],
            inputType: 'string'
        },
        {
            id: 'line3',
            displayName: 'Line 3',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 4],
            inputType: 'string'
        },
        {
            id: 'line4',
            displayName: 'Line 4',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 4],
            inputType: 'string'
        },
        {
            id: 'line5',
            displayName: 'Line 5',
            value: '',
            width: 12,
            displayForLabelTypes: [0],
            inputType: 'string'
        },
        {
            id: 'line6',
            displayName: 'Line 6',
            value: '',
            width: 12,
            displayForLabelTypes: [0],
            inputType: 'string'
        },
        {
            id: 'line7',
            displayName: 'Line 7',
            value: '',
            width: 12,
            displayForLabelTypes: [0],
            inputType: 'string'
        },
        {
            id: 'postalCode',
            displayName: 'Postal Code',
            value: '',
            width: 3,
            onOwnLine: true,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'country',
            displayName: 'Country',
            value: '',
            width: 9,
            onOwnLine: true,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'fromPCNumber',
            displayName: 'From PC Number',
            value: '',
            width: 3,
            displayForLabelTypes: [3],
            inputType: 'number'
        },
        {
            id: 'toPCNumber',
            displayName: 'To PC Number',
            value: '',
            width: 3,
            displayForLabelTypes: [3],
            inputType: 'number',
            onOwnLine: true
        },
        {
            id: 'poNumber',
            displayName: 'PO Number',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'number'
        },
        {
            id: 'partNumber',
            displayName: 'Part Number',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'string'
        },
        {
            id: 'qty',
            displayName: 'Qty',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'number'
        },
        {
            id: 'initials',
            displayName: 'Initials',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'string'
        },
        {
            id: 'date',
            displayName: 'Date',
            value: new Date().toDateString().slice(4),
            //`${date.getMonth()} ${date.getDay()}, ${date.getFullYear()}`,
            width: 6,
            displayForLabelTypes: [5],
            inputType: 'string',
            onOwnLine: true
        }
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
            console.info(newValue);
            setLabelType(parseInt(newValue, 10));
            //set label details/properties - rows etcccc
        }
        if (propertyName === 'printer') {
            setPrinter(newValue);
        }
        if (propertyName === 'quantity') {
            setQuantity(newValue);
        }
    };

    const handleLabelDetailsChange = (lineId, newValue) => {
        const rowToUpdate = labelDetails.findIndex(x => x.id === lineId);
        let updatedDetails = [...labelDetails];
        updatedDetails[rowToUpdate].value = newValue;
        setLabelDetails(updatedDetails);
    };

    const handleCopyFromAddress = newValue => {
        handleLabelDetailsChange('line1', newValue.line1);
        handleLabelDetailsChange('line2', newValue.line2);
        handleLabelDetailsChange('line3', newValue.line3);
        handleLabelDetailsChange('line4', newValue.line4);
        handleLabelDetailsChange('postalCode', newValue.postCode);
        handleLabelDetailsChange('country', newValue.country);
        handleLabelDetailsChange('addressee', newValue.addressee);
        handleLabelDetailsChange('addressee2', newValue.addressee2);
        handleLabelDetailsChange('addressId', newValue.id);
    };

    const handlePrintClick = () => {
        if (false) {
            print();
        }
        console.info(labelDetails);
        //send all details back - which type, printer and the deets
    };

    const handleClearClick = () => {
        setLabelDetails(printLinesInitialState);
    };

    const getInputValue = inputId => {
        const input = labelDetails.find(x => x.id === inputId);
        return input.value;
    };

    const getInputStyle = () => {
        let styleClass = '';
        if (labelType === 7) {
            styleClass = classes.boldText;
        }
        if (labelType === 3) {
            styleClass = classes.spacingRight;
        }
        return styleClass;
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
                                    <Grid
                                        item
                                        xs={12}
                                        container
                                        className={classes.filtersWithSpacing}
                                    >
                                        {/* className={classes.marginTop}> */}
                                        <Grid item xs={4} className={classes.spacingRight}>
                                            <Dropdown
                                                value={labelType || ''}
                                                label="Label Type"
                                                fullWidth
                                                items={labelPrintTypes.map(labelPrintType => ({
                                                    ...labelPrintType,
                                                    id: labelPrintType.id,
                                                    displayText: labelPrintType.name
                                                }))}
                                                onChange={handleFieldChange}
                                                propertyName="labelType"
                                                allowNoValue={false}
                                            />
                                        </Grid>
                                        <Grid item xs={5} className={classes.spacingRight}>
                                            <Dropdown
                                                value={printer || ''}
                                                label="Printer"
                                                fullWidth
                                                items={labelPrinters.map(labelPrinter => ({
                                                    ...labelPrinter,
                                                    id: labelPrinter.id,
                                                    displayText: labelPrinter.name
                                                }))}
                                                onChange={handleFieldChange}
                                                propertyName="printer"
                                                allowNoValue={false}
                                            />
                                        </Grid>
                                        <Grid item xs={2}>
                                            <InputField //Quantity
                                                label="Quantity"
                                                fullWidth
                                                type="number"
                                                onChange={handleFieldChange}
                                                propertyName="quantity"
                                                value={quantity}
                                            />
                                        </Grid>
                                    </Grid>

                                    <Grid
                                        container
                                        xs={12}
                                        className={
                                            labelType === 4 || labelType === 5 ? '' : classes.hide
                                        }
                                    >
                                        <Grid item xs={6}>
                                            <Typeahead
                                                onSelect={newValue => {
                                                    handleCopyFromAddress(newValue);
                                                }}
                                                propertyName="addressId"
                                                label="Address"
                                                modal
                                                items={addressSearchResults}
                                                value={getInputValue('addressId')}
                                                loading={addressSearchLoading}
                                                fetchItems={searchAddresses}
                                                links={false}
                                                clearSearch={() => clearAddressSearch}
                                                placeholder="Search for an Address"
                                                onChange={handleLabelDetailsChange}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Typeahead
                                                propertyName="supplierId"
                                                label="Supplier"
                                                modal
                                                items={supplierSearchResults}
                                                value={getInputValue('supplierId')}
                                                loading={supplierSearchLoading}
                                                fetchItems={searchSuppliers}
                                                links={false}
                                                clearSearch={() => clearSupplierSearch}
                                                placeholder="Search for a Supplier"
                                                onChange={handleLabelDetailsChange}
                                            />
                                        </Grid>
                                    </Grid>

                                    {labelDetails.map(
                                        line =>
                                            // if line is displayed for current label type
                                            line.displayForLabelTypes.includes(labelType) && (
                                                <Fragment>
                                                    <Grid item xs={line.width}>
                                                        {line.inputType !== 'typeahead' && (
                                                            <Fragment>
                                                                <InputField //inputs + logic
                                                                    label={line.displayName}
                                                                    fullWidth
                                                                    type={line.inputType}
                                                                    onChange={
                                                                        handleLabelDetailsChange
                                                                    }
                                                                    propertyName={line.id}
                                                                    value={line.value}
                                                                    className={getInputStyle()}
                                                                />
                                                            </Fragment>
                                                        )}
                                                    </Grid>
                                                    {line.onOwnLine && <Grid item xs={12} />}
                                                </Fragment>
                                            )
                                    )}
                                    <Grid item xs={6} className={classes.marginTop}>
                                        <Button
                                            onClick={handlePrintClick}
                                            variant="outlined"
                                            color="primary"
                                            className={classes.spacingRight}
                                        >
                                            Print
                                        </Button>
                                        <Button
                                            onClick={handleClearClick}
                                            variant="outlined"
                                            color="primary"
                                        >
                                            Clear
                                        </Button>
                                    </Grid>
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

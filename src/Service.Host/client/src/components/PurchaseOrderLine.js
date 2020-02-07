import React, { Fragment, useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown } from '@linn-it/linn-form-components-library';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

function PurchaseOrderLine({ detail, issueSernos, buildSernos, partNumber, orderNumber }) {
    const [details, setDetails] = useState({});
    const handleFieldChange = (propertyName, newValue) => {
        setDetails({ ...details, [propertyName]: newValue });
    };
    return (
        <Fragment key={details?.orderLine}>
            <Grid item xs={12}>
                <Typography variant="h6">{`Line ${detail?.orderLine}`}</Typography>
            </Grid>
            <Grid item xs={3}>
                <InputField fullWidth disabled value={detail?.partNumber} label="Part" />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={detail?.partDescription}
                    label="Description"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={detail?.orderQuantity}
                    label="Quantity Ordered"
                />
            </Grid>
            <Grid item xs={3}>
                <InputField fullWidth disabled value={detail?.ourUnitOfMeasure} label="Units" />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    fullWidth
                    items={['', 'Y', 'N']}
                    label="Issued"
                    value={detail?.issuedSerialNumbers}
                    propertyName="issuedSerialNumbers"
                    allowNoValue
                />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={1}>
                <InputField fullWidth disabled value={detail?.quantityReceived} label="Received" />
            </Grid>
            <Grid item xs={11} />
            <Grid item xs={1}>
                <InputField fullWidth disabled value={detail?.sernosIssued} label="Issued" />
            </Grid>
            <Grid item xs={3}>
                <InputField fullWidth disabled value={detail?.firstSernos} label="First Serial" />
            </Grid>
            <Grid item xs={3}>
                <InputField fullWidth disabled value={detail?.lastSernos} label="Last Serial" />
            </Grid>
            <Grid item xs={5} />
            <Grid item xs={1}>
                <InputField fullWidth disabled value={detail?.sernosBuilt} label="Built" />
            </Grid>
            <Grid item xs={11} />
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    type="number"
                    propertyName="qtyToIssue"
                    onChange={handleFieldChange}
                    value={details?.qtyToIssue}
                    label="Qty to Issue"
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    type="number"
                    propertyName="qtyToBuild"
                    onChange={handleFieldChange}
                    value={details?.qtyToBuild}
                    label="Qty to Build"
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    type="number"
                    value={details?.from}
                    label="From Serial"
                    propertyName="from"
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    type="number"
                    value={details?.to}
                    label="To Serial"
                    propertyName="to"
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={12}>
                <Button
                    onClick={() =>
                        buildSernos({
                            partNumber,
                            fromSerial: details?.fromSerial,
                            toSerial: details?.toSerial,
                            orderNumber
                        })
                    }
                    variant="outlined"
                    color="primary"
                >
                    Build
                </Button>
                <Button
                    onClick={() =>
                        issueSernos({
                            documentNumber: orderNumber,
                            documentType: 'PO',
                            documentLine: details?.DocumentLine,
                            partNumber,
                            quantity: details?.qtyToIssue
                        })
                    }
                    variant="outlined"
                    color="secondary"
                >
                    Issue
                </Button>
            </Grid>
        </Fragment>
    );
}

PurchaseOrderLine.propTypes = {
    item: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({}),
    issueSernos: PropTypes.func.isRequired,
    buildSernos: PropTypes.func.isRequired,
    updatePurchaseOrder: PropTypes.shape({}).isRequired,
    issueError: PropTypes.shape({}).isRequired,
    buildError: PropTypes.shape({}).isRequired,
    detail: PropTypes.shape({}).isRequired,
    partNumber: PropTypes.string.isRequired,
    orderNumber: PropTypes.number.isRequired
};

PurchaseOrderLine.defaultProps = {
    item: {},
    itemError: null
};

export default PurchaseOrderLine;

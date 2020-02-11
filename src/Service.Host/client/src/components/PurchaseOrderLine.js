import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown } from '@linn-it/linn-form-components-library';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';

function PurchaseOrderLine({ detail, issueSernos, buildSernos, partNumber, orderNumber }) {
    const [details, setDetails] = useState({});
    const handleFieldChange = (propertyName, newValue) => {
        setDetails({ ...details, [propertyName]: newValue });
    };

    const fromValid = () =>
        details?.from >= detail?.firstSernos && details?.from <= detail?.lastSernos;

    const buildInvalid = () => details?.qtyToBuild > detail?.sernosIssued - detail?.sernosBuilt;

    const issueInvalid = () => details?.qtyToIssue > detail?.orderQuantity - detail?.sernosIssued;

    useEffect(() => {
        if (details.qtyToBuild) {
            setDetails(d => ({
                ...d,
                from: detail?.firstSernos + detail?.sernosBuilt,
                to: detail?.firstSernos + details.qtyToBuild
            }));
        } else {
            setDetails(d => ({
                ...d,
                firstSernos: null,
                lastSernos: null
            }));
        }
    }, [details.qtyToBuild, detail, setDetails]);

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
                    items={['Y', 'N']}
                    label="Issued"
                    value={detail?.issuedSerialNumbers}
                    onChange={handleFieldChange}
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
                    error={details?.qtyToIssue && issueInvalid()}
                    helperText={
                        details?.qtyToIssue && issueInvalid()
                            ? 'Cannot issue more than order quantiy'
                            : ''
                    }
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
                    error={details?.qtyToBuild && buildInvalid()}
                    helperText={
                        details?.qtyToBuild && buildInvalid() ? 'Cannot build more than issued' : ''
                    }
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
                    error={details?.qtyToBuild && !fromValid()}
                    helperText={fromValid() ? '' : 'Must be within range'}
                    disabled={!details.qtyToBuild}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    type="number"
                    value={details?.from + details?.qtyToBuild}
                    label="To Serial"
                    propertyName="to"
                    onChange={handleFieldChange}
                    disabled
                />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={12}>
                <Button
                    onClick={() =>
                        issueSernos({
                            documentNumber: orderNumber,
                            documentType: 'PO',
                            documentLine: detail?.orderLine,
                            partNumber,
                            quantity: details?.qtyToIssue,
                            firstSerialNumber: detail?.firstSernos
                        })
                    }
                    disabled={!details?.qtyToIssue || issueInvalid()}
                    variant="outlined"
                    color="primary"
                >
                    Issue
                </Button>
                <Button
                    onClick={() =>
                        buildSernos({
                            partNumber,
                            fromSerial: details?.from,
                            toSerial: details?.from + details?.qtyToBuild,
                            orderNumber
                        })
                    }
                    variant="outlined"
                    disabled={!details?.qtyToBuild || buildInvalid()}
                    color="secondary"
                >
                    Build
                </Button>
            </Grid>
            <Grid item xs={12}>
                <Divider />
            </Grid>
        </Fragment>
    );
}

PurchaseOrderLine.propTypes = {
    item: PropTypes.shape({}),
    itemError: PropTypes.shape({}),
    issueSernos: PropTypes.func.isRequired,
    buildSernos: PropTypes.func.isRequired,
    detail: PropTypes.shape({}).isRequired,
    partNumber: PropTypes.string.isRequired,
    orderNumber: PropTypes.number.isRequired
};

PurchaseOrderLine.defaultProps = {
    item: {},
    itemError: null
};

export default PurchaseOrderLine;

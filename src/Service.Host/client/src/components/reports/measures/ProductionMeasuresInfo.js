import React from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Typography from '@material-ui/core/Typography';
import { Loading } from '@linn-it/linn-form-components-library';
import InfoButton from './InfoButton';

function ProductionMeasuresInfo(props) {
    const { infoData } = props;

    return (
        <InfoButton>
            {infoData ? (
                <Typography>
                    Last snapshot {moment(infoData.lastOSRRunDateTime).format('DD-MMM HH:mm')}
                    <br />
                    Trigger run {infoData.lastPtlJobref} {moment(infoData.lastPtlRunDateTime).format('DD-MMM HH:mm')}
                    <br />
                    Last Days to lookahead {infoData.lastDaysToLookAhead}
                </Typography>
            ) : (
                <Loading />
            )}
        </InfoButton>
    );
}

ProductionMeasuresInfo.propTypes = {
    infoData: PropTypes.shape({})
};

ProductionMeasuresInfo.defaultProps = {
    infoData: null
};

export default ProductionMeasuresInfo;

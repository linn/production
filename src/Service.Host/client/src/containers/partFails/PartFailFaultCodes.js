import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partFailFaultCodesActions from '../../actions/partFailFailFaultCodesActions';
import partFailFaultCodesSelectors from '../../selectors/partFailFaultCodesSelectors';
import * as itemTypes from '../../itemTypes';
import PartFailFaultCodes from '../../components/partFails/PartFailFaultCodes';

const mapStateToProps = state => ({
    items: partFailFaultCodesSelectors.getItems(state),
    loading: partFailFaultCodesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.partFailFaultCodes.item)
});

const initialise = () => dispatch => {
    dispatch(partFailFaultCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartFailFaultCodes));

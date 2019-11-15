import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AteFaultCodes from '../../components/ate/AteFaultCodes';
import ateFaultCodesActions from '../../actions/ateFaultCodesActions';
import ateFaultCodesSelectors from '../../selectors/ateFaultCodesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: ateFaultCodesSelectors.getItems(state),
    loading: ateFaultCodesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.ateFaultCodes.item)
});

const initialise = () => dispatch => {
    dispatch(ateFaultCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AteFaultCodes));

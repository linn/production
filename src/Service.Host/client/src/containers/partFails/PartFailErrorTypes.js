import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partFailErrorTypesActions from '../../actions/partFailErrorTypesActions';
import partFailErrorTypesSelectors from '../../selectors/partFailErrorTypesSelectors';
import * as itemTypes from '../../itemTypes';
import PartFailErrorTypes from '../../components/partFails/PartFailErrorTypes';

const mapStateToProps = state => ({
    items: partFailErrorTypesSelectors.getItems(state),
    loading: partFailErrorTypesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.partFailErrorTypes.item)
});

const initialise = () => dispatch => {
    dispatch(partFailErrorTypesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(PartFailErrorTypes));

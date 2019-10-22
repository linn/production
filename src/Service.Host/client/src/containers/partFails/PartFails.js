import { connect } from 'react-redux';
import PartFails from '../../components/partFails/PartFails';
import partFailsActions from '../../actions/partFailsActions';
import partFailsSelectors from '../../selectors/partFailsSelectors';

const mapStateToProps = state => ({
    items: partFailsSelectors.getSearchItems(state),
    loading: partFailsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: partFailsActions.search,
    clearSearch: partFailsActions.clearSearch,
    classes: {}
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(PartFails);
